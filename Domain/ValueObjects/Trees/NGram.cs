using Domain.Interfaces;

namespace Domain.ValueObjects.Trees
{
    public class NGram((int, double) rootValue) : Tree<(int, double), char>(rootValue), IProcessor
    {
        public void Process(ReadOnlySpan<char> window)
        {
            var currentNode = Root;
            Root.Value = (Root.Value.Item1 + 1, Root.Value.Item2);

            foreach (char c in window)
            {
                if (!currentNode.Children.TryGetValue(c, out var child))
                {
                    child = new Node<(int, double), char>((0, Double.NaN), currentNode);
                    currentNode.Children[c] = child;
                }

                child.Value = (child.Value.Item1 + 1, child.Value.Item2);
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

            var (count, probability) = targetNode.Value;
            if (double.IsNaN(probability))
            {
                var parentCount = targetNode.Parent?.Value.Item1 ?? 0;
                if (parentCount == 0)
                    return double.NaN;

                probability = count / (double)parentCount;
                targetNode.Value = (count, probability);
            }

            return probability;
        }
    }
}
