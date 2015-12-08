using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.Logic;

namespace Build.TestFramework
{
    public class SimpleMultiNode : IEquatable<SimpleMultiNode>
    {
        public SimpleMultiNode(string mainLeafId, int depth, List<SimpleNodeData> minorNodesData)
        {
            MainLeafId = mainLeafId;
            MinorNodesData = minorNodesData;
            Depth = depth;

            Nodes = new List<SimpleMultiNode>();
        }

        public string MainLeafId { get; set; }
        public List<SimpleNodeData> MinorNodesData { get; set; }
        public int Depth { get; set; }
        public List<SimpleMultiNode> Nodes { get; set; }

        public void Add(SimpleMultiNode simpleDoubleNode)
        {
            simpleDoubleNode.Depth = Depth + 1;
            Nodes.Add(simpleDoubleNode);
        }

        public override bool Equals(object obj)
        {
            var instance = obj as SimpleMultiNode;
            if (obj == null)
                return false;

            return Equals(instance);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (MainLeafId != null ? MainLeafId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (MinorNodesData != null ? MinorNodesData.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Depth;
                hashCode = (hashCode*397) ^ (Nodes != null ? Nodes.GetHashCode() : 0);
                return hashCode;
            }
        }

        public bool Equals(SimpleMultiNode other)
        {
            var result = MainLeafId == other.MainLeafId
                         && MinorLeafIds.SequenceEqual(other.MinorLeafIds)
                         && Depth == other.Depth
                         && ConnectionKind == other.ConnectionKind;
            var otherResult = Nodes.SequenceEqual(other.Nodes);
            return result && otherResult;
        }
    }
}