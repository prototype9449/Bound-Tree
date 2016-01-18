using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using BoundTree.Interfaces;
using BoundTree.Logic.Nodes;

namespace BoundTree.Logic.NodeData
{
    [Serializable]
    public class NodeData<T> : IEquatable<NodeData<T>> where T : IID<T>, new()
    {
        public int Depth { get; internal set; }
        private NodeInfo NodeInfo { get; set; }
        public T Id { get; internal set; }

        public NodeData()
        {
            Id = new T();
            NodeInfo = new Empty();
            Depth = -1;
        }

        public NodeData(T id, int depth, NodeInfo nodeInfo)
        {
            Contract.Requires(depth >= -1);
            Contract.Requires(nodeInfo != null);
            Contract.Requires(id != null);

            Id = id;
            Depth = depth;
            NodeInfo = nodeInfo;
        }

        public LogicLevel LogicLevel
        {
            get
            {
                Contract.Ensures(Contract.Result<LogicLevel>() != null);

                return NodeInfo.LogicLevel;
            }
        }

        public bool CanContain(NodeData<T> otherNodeData)
        {
            Contract.Requires(otherNodeData != null);

            return NodeInfo.CanContain(otherNodeData.NodeInfo);
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

        public static bool operator ==(NodeData<T> first, NodeData<T> second)
        {
            var objectFirst = (object)first;
            var objectSecond = (object) second;

            if (objectSecond == null && objectFirst == null)
                return true;

            return objectFirst != null && objectSecond != null && first.Equals(second);
        }

        public static bool operator !=(NodeData<T> first, NodeData<T> second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            var other = obj as NodeData<T>;
            if (other == null) return false;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Depth;
                hashCode = (hashCode * 397) ^ (NodeInfo != null ? NodeInfo.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(Id);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return IsEmpty() ? string.Format("({0})", Id) : Id.ToString();
        }

        public bool Equals(NodeData<T> other)
        {
            return Id.Equals(other.Id);
        }
    }
}