using System;
using System.Collections.Generic;
using BoundTree.Helpers;
using BoundTree.NodeInfo;

namespace BoundTree
{
    [Serializable]
    public class Node<T>
    {
        public T Id { get; private set; }
        public List<Node<T>> Nodes { get; internal set; }
        public INodeInfo NodeInfo { get; set; }
        internal int Deep { get; private set; }

        public Node(T id, INodeInfo nodeInfo, IList<Node<T>> nodes)
        {
            Deep = -1;
            Nodes = new List<Node<T>>(nodes);
            NodeInfo = nodeInfo;
            Id = id;
        }

        public Node(T id, INodeInfo nodeInfo) : this(id, nodeInfo, new List<Node<T>>())
        { }

        public Node(Node<T> node, INodeInfo nodeInfo) : this(node.Id, nodeInfo, new List<Node<T>>())
        { }

        internal void SetDeep(int initialDeep)
        {
            Deep = initialDeep + 1;
            Nodes.ForEach(node => node.SetDeep(Deep));
        }

        internal void IncreaseDeep()
        {
            SetDeep(Deep);
        }
    }
}
