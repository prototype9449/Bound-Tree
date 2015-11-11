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

        public SingleNode(SingleNode<T> singleNode, NodeInfo nodeInfo)
            : this(singleNode.Node.Id, nodeInfo)
        { }

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

        public void RecalculateDeep()
        {
            SetDeep(-1);
        }

        private void SetDeep(int initialDeep)
        {
            Node.Deep = initialDeep + 1;
            Nodes.ForEach(node => node.SetDeep(Node.Deep));
        }
    }
}
