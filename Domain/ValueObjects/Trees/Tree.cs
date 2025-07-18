using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects.Trees
{
    public class Tree<T, TKey>(T rootValue) where TKey : notnull
    {
        public Node<T, TKey> Root { get; } = new Node<T, TKey>(rootValue, null);
    }
}
