using System;
using System.Collections.Generic;
using BoundTree.Logic.Nodes;

namespace BoundTree.Logic
{
    [Serializable]
    public class SingleNode<T> where T : new()
    {
        public Node<T> Node { get; protected set; }
        public List<SingleNode<T>> Nodes { get; internal set; }

        public SingleNode(T id, NodeInfo nodeInfo, IList<SingleNode<T>> nodes)
        {
            Node = new Node<T>(id, -1, nodeInfo);
            Nodes = new List<SingleNode<T>>(nodes);
        }

        public SingleNode(T id, NodeInfo nodeInfo)
            : this(id, nodeInfo, new List<SingleNode<T>>())
        { }

        public SingleNode(T id, NodeInfo nodeInfo, int depth)
            : this(id, nodeInfo, new List<SingleNode<T>>())
        {
            Node.Depth = depth;
        }

        public SingleNode(SingleNode<T> singleNode, NodeInfo nodeInfo)
            : this(singleNode.Node.Id, nodeInfo)
        { }

        public void Add(SingleNode<T> singleNode)
        {
            singleNode.Node.Depth = this.Node.Depth + 1;
            Nodes.Add(singleNode);
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

                foreach (var node in queue.Dequeue().Nodes)
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

            if (root.Nodes.Count == 0) return;

            foreach (var node in root.Nodes)
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
            Nodes.ForEach(node => node.SetDeep(Node.Depth));
        }
    }
}
