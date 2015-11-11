using System;
using System.Collections.Generic;
using BoundTree.Logic.Nodes;

namespace BoundTree.Logic
{
    [Serializable]
    public class Node<T> : IEquatable<Node<T>> where T : new()
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

        public static bool operator ==(Node<T> first, Node<T> second)
        {
            var objectFirst = (object)first;
            var objectSecond = (object) second;

            if (objectSecond == null && objectFirst == null)
                return true;

            return objectFirst != null && objectSecond != null && first.Equals(second);
        }

        public static bool operator !=(Node<T> first, Node<T> second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Node<T>;
            if (other == null) return false;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Deep;
                hashCode = (hashCode * 397) ^ (NodeInfo != null ? NodeInfo.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(Id);
                return hashCode;
            }
        }


        public bool Equals(Node<T> other)
        {
            return Id.Equals(other.Id);
        }
    }
}