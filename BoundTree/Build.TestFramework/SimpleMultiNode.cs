﻿using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.Logic;
using BoundTree.Logic.NodeData;

namespace Build.TestFramework
{
    public class SimpleMultiNode : IEquatable<SimpleMultiNode>
    {
        private readonly bool _isEmpty;

        public SimpleMultiNode(string mainLeafId, int depth, bool isEmpty, List<SimpleNodeData> minorNodesData)
        {
            _isEmpty = isEmpty;
            MainLeafId = mainLeafId;
            MinorNodesData = minorNodesData;
            Depth = depth;

            Nodes = new List<SimpleMultiNode>();
        }

        public SimpleMultiNode(NodeData<StringId> nodeData, List<SimpleNodeData> minorNodesData)
        {
            _isEmpty = nodeData.IsEmpty();
            MainLeafId = nodeData.Id.ToString();
            MinorNodesData = minorNodesData;
            Depth = nodeData.Depth;

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

        public bool IsEmpty()
        {
            return _isEmpty;
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
            var areFieldsIdentical = 
                MainLeafId == other.MainLeafId
                && MinorNodesData.SequenceEqual(other.MinorNodesData)
                && Depth == other.Depth;

            var areNodesIdentical = Nodes.SequenceEqual(other.Nodes);
            return areFieldsIdentical && areNodesIdentical;
        }
    }
}