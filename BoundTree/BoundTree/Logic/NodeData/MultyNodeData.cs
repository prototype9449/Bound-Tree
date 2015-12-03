using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace BoundTree.Logic.NodeData
{
    public class MultiNodeData<T> : IEquatable<MultiNodeData<T>> where T : new()
    {
        private NodeData<T> NodeData { get; set; }
        private List<NodeData<T>> MinorDataNodes { get; set; }

        public MultiNodeData()
        {
            NodeData = new NodeData<T>();
            MinorDataNodes = new List<NodeData<T>>();
        }

        public MultiNodeData(NodeData<T> nodeData)
        {
            NodeData = nodeData;
            MinorDataNodes = new List<NodeData<T>>();
        }

        public MultiNodeData(NodeData<T> nodeData, List<NodeData<T>> minorDataNodes)
            : this(nodeData)
        {
            MinorDataNodes = minorDataNodes;
        }

        public bool CanContain(MultiNodeData<T> otherMultiNodeData)
        {
            Contract.Requires(NodeData != null);
            return NodeData.CanContain(NodeData);
        }

        public bool IsEmpty()
        {
            return NodeData.IsEmpty();
        }

        public LogicLevel LogicLevel
        {
            get
            {
                Contract.Requires(!NodeData.IsEmpty() || MinorDataNodes.Exists(node => !node.IsEmpty()));
                Contract.Ensures(Contract.Result<LogicLevel>() != null);

                if (NodeData.IsEmpty())
                {
                    return MinorDataNodes.First(node => !node.IsEmpty()).LogicLevel;
                }

                return NodeData.LogicLevel;
            }
        }

        public T Id
        {
            get { return NodeData.Id; }
        }

        public int Depth
        {
            get { return NodeData.Depth; }
            set { NodeData.Depth = value; }
        }

        public Type NodeType
        {
            get { return NodeData.GetType(); }
        }

        public static bool operator ==(MultiNodeData<T> first, MultiNodeData<T> second)
        {
            var objectFirst = (object)first;
            var objectSecond = (object)second;

            if (objectSecond == null && objectFirst == null)
                return true;

            return objectFirst != null && objectSecond != null && first.Equals(second);
        }

        public static bool operator !=(MultiNodeData<T> first, MultiNodeData<T> second)
        {
            return !(first == second);
        }

        public bool Equals(MultiNodeData<T> other)
        {
            return NodeData.Equals(other.NodeData);
        }

        public override bool Equals(object obj)
        {
            var other = obj as MultiNodeData<T>;
            if (other == null) return false;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return (NodeData != null ? NodeData.GetHashCode() : 0);
        }
    }
}