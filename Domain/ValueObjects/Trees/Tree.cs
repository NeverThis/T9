using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects.Trees
{
    public class Tree<T, TKey> where TKey : notnull
    {
        public Tree()
        {
            Root = new Node<T, TKey>();
        }

        public Tree(T rootValue)
        {
            RootValue = rootValue;
            Root = new Node<T, TKey>(rootValue, null);
        }

        public T RootValue { get; set; } = default!;
        public Node<T, TKey> Root { get; set; } = default!;
    }
}
