using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using BoundTree.Interfaces;
using BoundTree.Logic.NodeData;
using BoundTree.Logic.Nodes;

namespace BoundTree.Logic.TreeNodes
{
    [Serializable]
    public class SingleNode<T> : INode<T> where T : IId<T>, new()
    {
        public SingleNodeData<T> SingleNodeData { get; private set; }
        public List<SingleNode<T>> Childs { get; private set; }

        public SingleNode(T id, NodeInfo nodeInfo, IList<SingleNode<T>> nodes)
        {
            SingleNodeData = new SingleNodeData<T>(new NodeData<T>(id, -1, nodeInfo));
            Childs = new List<SingleNode<T>>(nodes);
        }

        public SingleNode(T id, NodeInfo nodeInfo)
            : this(id, nodeInfo, new List<SingleNode<T>>())
        { }

        public SingleNode(T id, NodeInfo nodeInfo, int depth)
        {
            SingleNodeData = new SingleNodeData<T>(new NodeData<T>(id, depth, nodeInfo));
            Childs = new List<SingleNode<T>>();
        }

        public T Id
        {
            get { return SingleNodeData.Id; }
            set { SingleNodeData.Id = value; }
        }

        public LogicLevel LogicLevel
        {
            get { return SingleNodeData.LogicLevel; }
        }

        public int Depth
        {
            get { return SingleNodeData.Depth; }
            set { SingleNodeData.Depth = value; }
        }

        public Type NodeType
        {
            get { return SingleNodeData.NodeType; }
        }

        public bool IsEmpty()
        {
            return SingleNodeData.IsEmpty();
        }

        public bool CanContain(SingleNode<T> singleNode)
        {
            return SingleNodeData.CanContain(singleNode.SingleNodeData);
        }

        public void Add(SingleNode<T> child)
        {
            Contract.Requires(child != null);

            child.SingleNodeData.Depth = SingleNodeData.Depth + 1;
            Childs.Add(child);
        }

        public SingleNode<T> GetById(T id)
        {
            var queue = new Queue<SingleNode<T>>();
            queue.Enqueue(this);
            while (queue.Count != 0)
            {
                if (queue.Peek().SingleNodeData.Id.Equals(id))
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
            SingleNodeData.Depth = initialDeep + 1;
            Childs.ForEach(node => node.SetDeep(SingleNodeData.Depth));
        }
    }
}
