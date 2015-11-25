using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.Logic;

namespace Build.TestFramework
{
    public class SimpleDoubleNode : IEquatable<SimpleDoubleNode>
    {
        public SimpleDoubleNode(string mainLeafId, string minorLeafId, ConnectionKind connectionKind, int depth)
        {
            MainLeafId = mainLeafId;
            MinorLeafId = minorLeafId;
            ConnectionKind = connectionKind;
            Depth = depth;

            Nodes = new List<SimpleDoubleNode>();
        }

        public string MainLeafId { get; set; }
        public string MinorLeafId { get; set; }
        public int Depth { get; set; }
        public ConnectionKind ConnectionKind { get; set; }
        public List<SimpleDoubleNode> Nodes { get; set; }

        public void Add(SimpleDoubleNode simpleDoubleNode)
        {
            simpleDoubleNode.Depth = Depth + 1;
            Nodes.Add(simpleDoubleNode);
        }

        public override bool Equals(object obj)
        {
            var instance = obj as SimpleDoubleNode;
            if (obj == null)
                return false;

            return Equals(instance);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (MainLeafId != null ? MainLeafId.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (MinorLeafId != null ? MinorLeafId.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Depth;
                hashCode = (hashCode*397) ^ (int) ConnectionKind;
                hashCode = (hashCode*397) ^ (Nodes != null ? Nodes.GetHashCode() : 0);
                return hashCode;
            }
        }

        public bool Equals(SimpleDoubleNode other)
        {
            var result = MainLeafId == other.MainLeafId
                         && MinorLeafId == other.MinorLeafId
                         && Depth == other.Depth
                         && ConnectionKind == other.ConnectionKind;
            var otherResult = Nodes.SequenceEqual(other.Nodes);
            return result && otherResult;
        }
    }
}