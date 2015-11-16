using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundTree.Logic.ConnectionKinds
{
    public abstract class Connection : IEquatable<Connection>
    {
        public abstract ConnectionKind ConnectionKind { get; }

        public abstract string GetSign(bool isLeft);

        public override bool Equals(object otherObj)
        {
            var obj = otherObj as Connection;

            if (obj == null) 
                return false;

            return this.Equals(obj);

        }

        public bool Equals(Connection other)
        {
            if(other as Object == null) 
                return false;

            return other.ConnectionKind == this.ConnectionKind;
        }

        public static bool operator ==(Connection firstConnection, Connection secondConnection)
        {
            var first = firstConnection as Object;
            var second = secondConnection as Object;

            if(first == null && second == null) 
                return true;

            if(first == null || second == null)
                return false;

            return firstConnection.Equals(secondConnection);
        }

        public static bool operator !=(Connection firstConnection, Connection secondConnection)
        {
            return !(firstConnection == secondConnection);
        }
    }
}
