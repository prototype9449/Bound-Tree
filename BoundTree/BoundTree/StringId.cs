using System;

namespace BoundTree
{
    [Serializable]
    public class StringId : IEquatable<StringId>
    {
        private readonly string _id;

        public StringId(string id)
        {
            _id = id;
        }

        public StringId()
        {
            _id = "";
        }

        public bool Equals(StringId other)
        {
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
    }
}