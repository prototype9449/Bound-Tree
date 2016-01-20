using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using BoundTree.Interfaces;
using BoundTree.Logic.NodeData;

namespace BoundTree.Logic.TreeNodes
{
    [Serializable]
    public class MultiNode<T> : INode<T> where T : IID<T>,new()
    {
        public MultiNodeData<T> MultiNodeData { get; private set; }
        public List<MultiNode<T>> Childs { get; private set; }

        public MultiNode(MultiNodeData<T> multiNodeData, List<MultiNode<T>> childs)
        {
            MultiNodeData = multiNodeData;
            Childs = childs;
        }

        public MultiNode(SingleNode<T> singleNode, NodeInfoFactory nodeInfoFactory)
        {
            MultiNodeData = new MultiNodeData<T>(singleNode.SingleNodeData);
            Childs = new List<MultiNode<T>>();
            singleNode.Childs.ForEach(node => Childs.Add(new MultiNode<T>(node, nodeInfoFactory)));
        }

        public LogicLevel LogicLevel
        {
            get { return MultiNodeData.LogicLevel; }
        }

        public int Depth
        {
            get { return MultiNodeData.Depth; }
            set { MultiNodeData.Depth = value; }
        }

        public Type NodeType
        {
            get { return MultiNodeData.NodeType; }
        }

        public bool IsEmpty()
        {
            return MultiNodeData.IsEmpty();
        }

        public T Id
        {
            get { return MultiNodeData.Id; }
            set { MultiNodeData.Id = value; }
        }

        public void Add(MultiNode<T> child)
        {
            Contract.Requires(child != null);
            Contract.Ensures(Childs.Count - Contract.OldValue(Childs.Count) == 1);

            Childs.Add(child);
        }

        public MultiNode<T> GetById(T id)
        {
            var queue = new Queue<MultiNode<T>>();
            queue.Enqueue(this);
            while (queue.Count != 0)
            {
                if (queue.Peek().Id.Equals(id))
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

        public List<MultiNode<T>> ToList()
        {
            var nodes = new List<MultiNode<T>>();
            RecursiveFillNodes(this, nodes);
            return nodes;
        }

        private void RecursiveFillNodes(MultiNode<T> root, List<MultiNode<T>> nodes)
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
            MultiNodeData.Depth = initialDeep + 1;
            Childs.ForEach(node => node.SetDeep(MultiNodeData.Depth));
        }
    }
}