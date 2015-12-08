using System;
using BoundTree.Logic;

namespace Build.TestFramework
{
    public class SimpleNodeData : IEquatable<SimpleNodeData>
    {
        public SimpleNodeData(ConnectionKind connectionKind, string id)
        {
            ConnectionKind = connectionKind;
            Id = id;
        }

        public string Id { get; set; }
        public ConnectionKind ConnectionKind { get; set; }

        public bool Equals(SimpleNodeData other)
        {
            return Id == other.Id && ConnectionKind == other.ConnectionKind;
        }
    }
}