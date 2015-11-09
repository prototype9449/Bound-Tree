using System;
using System.Collections.Generic;
using BoundTree.NodeInfo;

namespace BoundTree.Logic
{
    [Serializable]
    public class SingleNode<T> where T : new()
    {
        public Node<T> Node { get; protected set; }
        public List<SingleNode<T>> Nodes { get; internal set; }

        public SingleNode(T id, INodeInfo nodeInfo, IList<SingleNode<T>> nodes)
        {
            Node = new Node<T>(id, -1, nodeInfo);
            Nodes = new List<SingleNode<T>>(nodes);
        }

        public SingleNode(T id, INodeInfo nodeInfo) : this(id, nodeInfo, new List<SingleNode<T>>())
        { }

        public SingleNode(SingleNode<T> singleNode, INodeInfo nodeInfo) : this(singleNode.Node.Id, nodeInfo)
        { }

        internal void SetDeep(int initialDeep)
        {
            Node.Deep = initialDeep + 1;
            Nodes.ForEach(node => node.SetDeep(Node.Deep));
        }

        internal void IncreaseDeep()
        {
            SetDeep(Node.Deep);
        }
    }
}
