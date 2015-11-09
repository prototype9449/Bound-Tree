using System;
using BoundTree.NodeInfo;

namespace BoundTree.Logic
{
    [Serializable]
    public class Node<T> where T : new()
    {
        public int Deep { get; internal set; }
        public INodeInfo NodeInfo { get; protected set; }
        public T Id { get; protected set; }

        public Node()
        {
            Id = new T();
            NodeInfo = new EmptyNodeInfo();
            Deep = -1;
        }

        public Node(T id, int deep, INodeInfo nodeInfo)
        {
            Id = id;
            Deep = deep;
            NodeInfo = nodeInfo;
        }

        public int LogicLevel
        {
            get
            {
                return NodeInfo.LogicLevel;
            }
        }

        public bool IsEmpty()
        {
            return NodeInfo.IsEmpty();
        }

        public Type NodeType
        {
            get
            {
                return NodeInfo.GetType();
            }
        }
    }
}