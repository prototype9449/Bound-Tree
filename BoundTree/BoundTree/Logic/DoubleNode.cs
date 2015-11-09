using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.NodeInfo;

namespace BoundTree.Logic
{
    public class DoubleNode<T> where T : class, IEquatable<T>, new()
    {
        public Node<T> MainLeaf { get; set; }
        public Node<T> MinorLeaf { get; set; }
        internal Node<T> Shadow { get; set; }
        public ConnectionKind ConnectionKind { get; set; }
        public List<DoubleNode<T>> Nodes { get; set; }
        public int Deep { get; set; }

        public int LogicLevel
        {
            get
            {
                if (MinorLeaf.IsEmpty())
                    return MainLeaf.LogicLevel;

                return MinorLeaf.LogicLevel;
            }
        }

        public bool IsMinorEmpty()
        {
            return MinorLeaf.IsEmpty();
        }

        public Node<T> GetMinorValue()
        {
            return MinorLeaf ?? Shadow;
        }
        
        public DoubleNode()
        {
            MainLeaf = new Node<T>(new T(), -1, new EmptyNodeInfo());
            MinorLeaf = new Node<T>(new T(), -1, new EmptyNodeInfo());
            Nodes = new List<DoubleNode<T>>();
        }

        public DoubleNode(Node<T> mainLeaf, Node<T> minorLeaf) : this()
        {
            MainLeaf = mainLeaf;
            MinorLeaf = minorLeaf;
        }

        public DoubleNode(Node<T> mainLeaf) : this()
        {
            MainLeaf = mainLeaf;
        }

        public DoubleNode(SingleNode<T> singleNode) : this(singleNode.Node)
        {
        }

        public void Add(DoubleNode<T> doubleNode)
        {
            doubleNode.Deep += Deep + 1;
            Nodes.Add(doubleNode);
        }

        public DoubleNode<T> GetLonelyChild()
        {
            if (Nodes.Count() != 1 || Nodes.First().MinorLeaf.IsEmpty())
            {
                return null;
            }

            return Nodes.First();
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