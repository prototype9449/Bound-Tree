using System;
using BoundTree.NodeInfo;

namespace BoundTree
{
    public class Cortege<T> where T : class, IEquatable<T>
    {
        public INodeInfo NodeInfo { get; set; }
        public T Id { get; private set; }

        public Cortege() { }

        public Cortege(T id, INodeInfo nodeInfo) : this()
        {
            Id = id;
            NodeInfo = nodeInfo;
        }

        public Cortege(Node<T> node) : this()
        {
            Id = node.Id;
            NodeInfo = node.NodeInfo;
        }
    }
}
