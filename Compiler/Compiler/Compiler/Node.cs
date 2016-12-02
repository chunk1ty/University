namespace Compiler
{
    using System;

    public class Node<T,V> where T : IComparable<T>
    {
        public T Symbol { get; set; }
        public V Code { get; set; }
        public Node<T,V> Left { get; set; }
        public Node<T,V> Right { get; set; }

        public override string ToString()
        {
            return "     "+Symbol.ToString() + " -> " + Code.ToString()+"     ";
        }
    }
}
