﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Interfaces;
using BoundTree.Logic.NodeData;

namespace BoundTree.Logic.TreeNodes
{
    public class DoubleNode<T> where T : class, IEquatable<T>, IID<T>, new()
    {
        public MultiNodeData<T> MainLeaf { get; private set; }
        public SingleNodeData<T> MinorLeaf { get; set; }
        internal SingleNodeData<T> Shadow { get; set; }

        public ConnectionKind ConnectionKind { get; set; }
        public List<DoubleNode<T>> Nodes { get; internal set; }
        public int Depth { get; private set; }

        private DoubleNode()
        {
            Nodes = new List<DoubleNode<T>>();
        }

        public DoubleNode(MultiNodeData<T> mainLeaf, SingleNodeData<T> minorLeaf)
            : this()
        {
            Contract.Requires(mainLeaf != null);
            Contract.Requires(minorLeaf != null);

            MainLeaf = mainLeaf;
            MinorLeaf = minorLeaf;
        }

        public DoubleNode(MultiNode<T> multiNode)
            : this()
        {
            Contract.Requires(multiNode != null);

            MainLeaf = multiNode.MultiNodeData;
            MinorLeaf = new SingleNodeData<T>();
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

        public MultiNode<T> ToMultiNode()
        {
            Contract.Requires(MainLeaf != null);
            Contract.Requires(MinorLeaf != null);

            var minorDataNodes = new List<ConnectionNodeData<T>>();
            minorDataNodes.AddRange(MainLeaf.MinorDataNodes);
            minorDataNodes.Add(new ConnectionNodeData<T>(ConnectionKind, MinorLeaf.NodeData));
            var multiNodeData = new MultiNodeData<T>(MainLeaf.NodeData, minorDataNodes);

            var multiNode = new MultiNode<T>(multiNodeData, new List<MultiNode<T>>());
            Nodes.ForEach(doubleNode => multiNode.Add(doubleNode.ToMultiNode()));

            return multiNode;
        }

        public bool IsMinorEmpty()
        {
            return MinorLeaf.IsEmpty();
        }

        public SingleNodeData<T> GetMinorValue()
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

            doubleNode.Depth += Depth + 1;
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
            Depth = -1;
            SetDeep(-1);
        }

        private void SetDeep(int initialDeep)
        {
            Depth = initialDeep + 1;
            Nodes.ForEach(node => node.SetDeep(this.Depth));
        }
    }
}