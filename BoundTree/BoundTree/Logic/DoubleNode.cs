using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Logic.Nodes;

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

        public DoubleNode()
        {
            MainLeaf = new Node<T>(new T(), -1, new Empty());
            MinorLeaf = new Node<T>(new T(), -1, new Empty());
            Nodes = new List<DoubleNode<T>>();
        }

        public DoubleNode(Node<T> mainLeaf, Node<T> minorLeaf)
            : this()
        {
            Contract.Requires(mainLeaf != null);
            Contract.Requires(minorLeaf != null);

            MainLeaf = mainLeaf;
            MinorLeaf = minorLeaf;
        }

        public DoubleNode(Node<T> mainLeaf)
            : this()
        {
            Contract.Requires(mainLeaf != null);

            MainLeaf = mainLeaf;
        }

        public DoubleNode(SingleNode<T> singleNode)
            : this(singleNode.Node)
        {
            Contract.Requires(singleNode != null);
        }

        public LogicLevel LogicLevel
        {
            get
            {
                Contract.Ensures(Contract.Result<LogicLevel>() != null);

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
            if (MinorLeaf.IsEmpty())
            {
                return Shadow;
            }
            return MinorLeaf;
        }

        public void Add(DoubleNode<T> doubleNode)
        {
            Contract.Requires(doubleNode != null);

            doubleNode.Deep += Deep + 1;
            Nodes.Add(doubleNode);
        }

        public DoubleNode<T> GetLonelyChild()
        {
            if (Nodes.Count() != 1 || Nodes.First().IsMinorEmpty())
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

        public void RecalculateDeep()
        {
            Deep = -1;
            SetDeep(-1);
        }

        private void SetDeep(int initialDeep)
        {
            Deep = initialDeep + 1;
            Nodes.ForEach(node => node.SetDeep(this.Deep));
        }

       
    }
}