using Domain.Interfaces;
using System.Text.Json.Serialization;

namespace Domain.ValueObjects.Trees
{
    public class NGram(ModelData rootValue) : Tree<ModelData, char>(rootValue), IProcessor
    {

        public void Process(ReadOnlySpan<char> window)
        {
            var currentNode = Root;
            Root.Value.Occurrences++;

            foreach (char c in window)
            {
                if (!currentNode.Children.TryGetValue(c, out var child))
                {
                    child = new Node<ModelData, char>(new ModelData(), currentNode);
                    currentNode.Children[c] = child;
                }

                child.Value.Occurrences++;
                currentNode = child;
            }
        }

        public double GetConditionalProbability(string givenSequence, char charToEvaluate)
        {
            var currentNode = Root;

            foreach (char c in givenSequence)
            {
                if (!currentNode.Children.TryGetValue(c, out var nextNode))
                    return double.NaN;

                currentNode = nextNode;
            }

            if (!currentNode.Children.TryGetValue(charToEvaluate, out var targetNode))
                return double.NaN;

            if (double.IsNaN(targetNode.Value.Probability))
            {
                var parentCount = targetNode.Parent?.Value.Occurrences ?? 0;
                if (parentCount == 0)
                    return double.NaN;

                targetNode.Value.Probability = targetNode.Value.Occurrences / (double)parentCount;
            }

            return targetNode.Value.Probability;
        }

        public ModelData RootValue => base.RootValue;
    }
}
