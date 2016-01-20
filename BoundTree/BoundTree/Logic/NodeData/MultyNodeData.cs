using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Interfaces;

namespace BoundTree.Logic.NodeData
{
    [Serializable]
    public class MultiNodeData<T> : IEquatable<MultiNodeData<T>> where T : IID<T>, new()
    {
        public NodeData<T> NodeData { get; private set; }
        public List<ConnectionNodeData<T>> MinorDataNodes { get; private set; }

        private MultiNodeData(NodeInfoFactory nodeInfoFactory)
        {
            NodeData = new NodeData<T>(nodeInfoFactory);
            MinorDataNodes = new List<ConnectionNodeData<T>>();
        }

        public MultiNodeData(int width, NodeInfoFactory nodeInfoFactory)
            : this(nodeInfoFactory)
        {
            for (int i = 0; i < width; i++)
            {
                MinorDataNodes.Add(new ConnectionNodeData<T>(ConnectionKind.None, new NodeData<T>(nodeInfoFactory)));
            }
        }

        public MultiNodeData(SingleNodeData<T> singleNodeData)
            : this(singleNodeData.NodeData)
        { }

        public MultiNodeData(NodeData<T> nodeData)
        {
            NodeData = nodeData;
            MinorDataNodes = new List<ConnectionNodeData<T>>();
        }

        public MultiNodeData(NodeData<T> nodeData, List<ConnectionNodeData<T>> minorDataNodes)
            : this(nodeData)
        {
            MinorDataNodes = minorDataNodes;
        }

        public int Width { get { return MinorDataNodes.Count; } }

        public bool IsEmpty()
        {
            return NodeData.IsEmpty();
        }

        public Type NodeType
        {
            get { return NodeData.NodeType; }
        }

        public LogicLevel LogicLevel
        {
            get
            {
                Contract.Ensures(Contract.Result<LogicLevel>() != null);

                if (NodeData.IsEmpty())
                {
                    var connectionDataNode = MinorDataNodes.FirstOrDefault(node => !node.NodeData.IsEmpty());
                    return connectionDataNode == null ? new LogicLevel() : connectionDataNode.NodeData.LogicLevel;
                }

                return NodeData.LogicLevel;
            }
        }

        public T Id
        {
            get { return NodeData.Id; }
            set { NodeData.Id = value; }
        }

        public int Depth
        {
            get { return NodeData.Depth; }
            set { NodeData.Depth = value; }
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