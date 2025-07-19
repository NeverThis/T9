using System.Text.Json.Serialization;

namespace Domain.ValueObjects.Trees
{
    public class Node<T, TKey> where TKey : notnull
    {
        public Node()
        {
            Children = [];
        }

        public Node(T value, Node<T, TKey>? parent)
        {
            Value = value;
            Parent = parent;
            Children = [];
        }

        public T Value { get; set; } = default!;
        public Node<T, TKey>? Parent { get; set; }
        public Dictionary<TKey, Node<T, TKey>> Children { get; set; } = new();
    }
}
