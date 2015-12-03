using System;
using System.Collections.Generic;
using BoundTree.Logic.NodeData;

namespace BoundTree.Logic.TreeNodes
{
    public class MultiNode<T> : INode<T> where T : new()
    {
        public MultiNodeData<T> MultiNodeData { get; set; }
        public List<MultiNode<T>> Childs { get; set; }

        public MultiNode(SingleNodeData<T> singleNode)
        {
            MultiNodeData = new MultiNodeData<T>(singleNode.NodeData);
            Childs = new List<MultiNode<T>>();
        }

        public LogicLevel LogicLevel
        {
            get
            {
                return MultiNodeData.LogicLevel;
            }
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

        public T Id
        {
            get { return MultiNodeData.Id; }
        }

        public bool CanContain(MultiNodeData<T> multiNodeData)
        {
            return multiNodeData.CanContain(multiNodeData);
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
    }
}