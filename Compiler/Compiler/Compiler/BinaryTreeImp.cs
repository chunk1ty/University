namespace Compiler
{
    using System;
    using System.Text;
    using System.Collections;
    public class BinaryTreeImp<T,V> where T : IComparable<T>
    {
        public Node<T, V> Root { get; set; }

        public Node<T,V> FindTab(T symbol, V code, BinaryTreeImp<T,V> tree)
        {
            var node = this.Root;

            while (node != null)
            {
                
                if (node.Symbol.CompareTo(symbol) == 0)
                {
                    return node;
                }
                //search right
                else if (node.Symbol.CompareTo(symbol) < 0)
                {
                    node = node.Right;                   
                }
                //search left
                else
                {
                    node = node.Left;
                }
            }

            tree.Add(symbol, code);
            return FindTab(symbol, code, tree);
        }

        public void Add(T symbol, V code)
        {
            var newNode = new Node<T,V>()
            {
                Symbol = symbol,
                Code = code
            };

            if (this.Root == null)
            {
                this.Root = newNode;
            }
            else
            {
                var parent = this.FindParentOf(symbol, code);
                if (parent.Symbol.CompareTo(symbol) < 0)
                {
                    parent.Right = newNode;
                }
                else
                {
                    parent.Left = newNode;
                }
            }

        }

        private Node<T,V> FindParentOf(T symbol, V code)
        {
            var node = this.Root;
            while (true)
            {
                //go right
                if (node.Symbol.CompareTo(symbol) < 0)
                {
                    if (node.Right == null)
                    {
                        return node;
                    }
                    else
                    {
                        node = node.Right;
                    }
                }
                //go left
                else
                {
                    if (node.Left == null)
                    {
                        return node;
                    }
                    else
                    {
                        node = node.Left;
                    }
                }
            }
        }
    }
}
