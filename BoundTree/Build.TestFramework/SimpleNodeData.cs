using System;
using System.Diagnostics.Contracts;
using BoundTree.Logic;

namespace Build.TestFramework
{
    public class SimpleNodeData : IEquatable<SimpleNodeData>
    {
        private readonly bool _isEmpty;

        public SimpleNodeData(ConnectionKind connectionKind, string id, bool isEmpty)
        {
            _isEmpty = isEmpty;
            Contract.Requires(!string.IsNullOrEmpty(id));

            ConnectionKind = connectionKind;
            Id = id;
        }

        public string Id { get; set; }
        public ConnectionKind ConnectionKind { get; set; }

        public bool IsEmpty()
        {
            return _isEmpty;
        }

        public bool Equals(SimpleNodeData other)
        {
            return Id == other.Id && ConnectionKind == other.ConnectionKind;
        }

        public override bool Equals(object obj)
        {
            var simpleNodeData = obj as SimpleNodeData;

            if (ReferenceEquals(simpleNodeData, null))
                return false;

            return Equals(simpleNodeData);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Id != null ? Id.GetHashCode() : 0)*397) ^ (int) ConnectionKind;
            }
        }
    }
}