namespace Domain.ValueObjects.Trees
{
    public class Node<T, TKey>(T value, Node<T, TKey>? parent) where TKey : notnull
    {
        public T Value { get; set; } = value;
        public Node<T, TKey>? Parent { get; set; } = parent;
        public Dictionary<TKey, Node<T, TKey>> Children { get; set; } = [];
    }
}
