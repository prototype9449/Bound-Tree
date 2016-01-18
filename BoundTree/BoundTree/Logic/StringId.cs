using System;
using System.Diagnostics.Contracts;
using BoundTree.Interfaces;

namespace BoundTree.Logic
{
    [Serializable]
    public class StringId : IEquatable<StringId>, IID<StringId>
    {
        private readonly string _id;

        public StringId(string id)
        {
            Contract.Ensures(id != null);

            _id = id;
        }

        public StringId()
        {
            _id = "";
        }

        public bool Equals(StringId other)
        {
            Contract.Requires(other != null);

            return _id == other._id;
        }

        public override bool Equals(object obj)
        {
            var other = obj as StringId;
            if (other == null) return false;
            return other._id == _id;
        }

        public override int GetHashCode()
        {
            return (_id != null ? _id.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return _id;
        }

        public bool IsEmpty()
        {
            return _id == "";
        }
    }
}