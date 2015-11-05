using System;
using System.Collections.Generic;

namespace BoundTree
{
    public class DoubleNode<T> where T : class, IEquatable<T>
    {
        public Cortege<T> MainLeaf { get; set; }
        public Cortege<T> MinorLeaf { get; set; }
        public ConnectionKind ConnectionKind { get; set; }
        public List<DoubleNode<T>> Nodes { get; set; }
        public int Deep { get; set; }

        public DoubleNode()
        {
            MainLeaf = new Cortege<T>();
            MinorLeaf = new Cortege<T>();
            Nodes = new List<DoubleNode<T>>();
        }

        public DoubleNode(Cortege<T> mainLeaf, Cortege<T> minorLeaf) : this()
        {
            MainLeaf = mainLeaf;
            MinorLeaf = minorLeaf;
        }

        public DoubleNode(Cortege<T> mainLeaf) : this()
        {
            MainLeaf = mainLeaf;
        }

        public DoubleNode(Node<T> node) : this(new Cortege<T>(node)) { }

        public void Add(DoubleNode<T> doubleNode)
        {
            doubleNode.Deep+=Deep + 1;
            Nodes.Add(doubleNode);
        }
        
        public List<DoubleNode<T>> ToList()
        {
            var nodes = new List<DoubleNode<T>>();
            RecursiveFillNodes(this, nodes);
            return nodes;
        }

        private void RecursiveFillNodes(DoubleNode<T> root, List<DoubleNode<T>> nodes)
        {
            nodes.Add(root);

            if (root.Nodes.Count == 0) return;

            foreach (var node in root.Nodes)
            {
                RecursiveFillNodes(node, nodes);
            }
        }
    }

    public enum ConnectionKind
    {
        Strict,
        Relative,
        None
    }
}
