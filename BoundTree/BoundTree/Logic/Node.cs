using System;
using BoundTree.Logic.Nodes;

namespace BoundTree.Logic
{
    [Serializable]
    public class Node<T> where T : new()
    {
        public int Deep { get; internal set; }
        public NodeInfo NodeInfo { get; protected set; }
        public T Id { get; protected set; }

        public Node()
        {
            Id = new T();
            NodeInfo = new Empty();
            Deep = -1;
        }

        public Node(T id, int deep, NodeInfo nodeInfo)
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