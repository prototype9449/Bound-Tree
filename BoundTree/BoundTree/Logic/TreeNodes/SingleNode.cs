﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using BoundTree.Logic.Nodes;

namespace BoundTree.Logic.TreeNodes
{
    [Serializable]
    public class SingleNode<T> where T : new()
    {
        public Node<T> Node { get; private set; }
        public List<SingleNode<T>> Childs { get; private set; }

        public SingleNode(T id, NodeInfo nodeInfo, IList<SingleNode<T>> nodes)
        {
            Node = new Node<T>(id, -1, nodeInfo);
            Childs = new List<SingleNode<T>>(nodes);
        }

        public SingleNode(T id, NodeInfo nodeInfo)
            : this(id, nodeInfo, new List<SingleNode<T>>())
        { }

        public SingleNode(T id, NodeInfo nodeInfo, int depth)
            : this(id, nodeInfo, new List<SingleNode<T>>())
        {
            Node.Depth = depth;
        }

        public void Add(SingleNode<T> child)
        {
            Contract.Requires(child != null);

            child.Node.Depth = this.Node.Depth + 1;
            Childs.Add(child);
        }

        public SingleNode<T> GetById(T id)
        {
            var queue = new Queue<SingleNode<T>>();
            queue.Enqueue(this);
            while (queue.Count != 0)
            {
                if (queue.Peek().Node.Id.Equals(id))
                {
                    return queue.Peek();
                }

                foreach (var node in queue.Dequeue().Childs)
                {
                    queue.Enqueue(node);
                }
            }

            return null;
        }


        public List<SingleNode<T>> ToList()
        {
            var nodes = new List<SingleNode<T>>();
            RecursiveFillNodes(this, nodes);
            return nodes;
        }

        private void RecursiveFillNodes(SingleNode<T> root, List<SingleNode<T>> nodes)
        {
            nodes.Add(root);

            if (root.Childs.Count == 0) return;

            foreach (var node in root.Childs)
            {
                RecursiveFillNodes(node, nodes);
            }
        }

        public void RecalculateDeep()
        {
            SetDeep(-1);
        }

        private void SetDeep(int initialDeep)
        {
            Node.Depth = initialDeep + 1;
            Childs.ForEach(node => node.SetDeep(Node.Depth));
        }
    }
}
