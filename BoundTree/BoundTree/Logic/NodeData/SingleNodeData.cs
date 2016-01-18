using System;
using System.Diagnostics.Contracts;
using BoundTree.Interfaces;

namespace BoundTree.Logic.NodeData
{
    [Serializable]
    public class SingleNodeData<T> : IEquatable<SingleNodeData<T>> where T : IID<T>, new()
    {
        public NodeData<T> NodeData { get; set; }

        public SingleNodeData()
        {
            NodeData = new NodeData<T>();
        }

        public SingleNodeData(NodeData<T> nodeData)
        {
            NodeData = nodeData;
        }

        public int Depth
        {
            get { return NodeData.Depth; }
            set
            {
                Contract.Requires(value >= -1);
                NodeData.Depth = value;
            }
        }

        public LogicLevel LogicLevel
        {
            get
            {
                Contract.Ensures(Contract.Result<LogicLevel>() != null);
                return NodeData.LogicLevel;
            }
        }

        public T Id
        {
            get { return NodeData.Id; }
            set { NodeData.Id = value; }
        }

        public Type NodeType
        {
            get { return NodeData.NodeType; }
        }

        public bool CanContain(SingleNodeData<T> otherNodeData)
        {
            Contract.Requires(otherNodeData != null);
            return NodeData.CanContain(otherNodeData.NodeData);
        }

        public bool IsEmpty()
        {
            return NodeData.IsEmpty();
        }

        public static bool operator ==(SingleNodeData<T> first, SingleNodeData<T> second)
        {
            var objectFirst = (object)first;
            var objectSecond = (object)second;

            if (objectSecond == null && objectFirst == null)
                return true;

            return objectFirst != null && objectSecond != null && first.Equals(second);
        }

        public static bool operator !=(SingleNodeData<T> first, SingleNodeData<T> second)
        {
            return !(first == second);
        }

        public bool Equals(SingleNodeData<T> other)
        {
            return NodeData.Equals(other.NodeData);
        }

        public override bool Equals(object obj)
        {
            var other = obj as NodeData<T>;
            if (other == null) return false;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return (NodeData != null ? NodeData.GetHashCode() : 0);
        }
    }
}