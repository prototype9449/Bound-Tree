﻿using System;
using BoundTree.Interfaces;

namespace BoundTree.Logic.NodeData
{
    [Serializable]
    public class ConnectionNodeData<T> where T : IID<T>,new()
    {
        public ConnectionNodeData(ConnectionKind connectionKind, NodeData<T> nodeData)
        {
            ConnectionKind = connectionKind;
            NodeData = nodeData;
        }

        public NodeData<T> NodeData { get; private set; }
        public ConnectionKind ConnectionKind { get; private set; }

        public bool IsEmpty()
        {
            return NodeData.IsEmpty();
        }
    }
}