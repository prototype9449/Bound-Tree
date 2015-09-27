using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BoundTree.Interfaces;

namespace BoundTree
{
    public class Identificator : IEquatable<Identificator>
    {
        private readonly IList<int> _orderIds;
        public IList<int> OrderIds
        {
            get { return _orderIds; }
        }

        public Identificator(IList<int> orderIds)
        {
            _orderIds = orderIds;
        }
        
        public bool NeedToInsert(Identificator identificator)
        {
            var lengthIds = _orderIds.Count;
            return _orderIds.Count < identificator.OrderIds.Count && _orderIds.SequenceEqual(identificator.OrderIds.Take(lengthIds));
        }

        public bool Equals(Identificator other)
        {
            return this.OrderIds.SequenceEqual(other.OrderIds);
        }

        public static bool operator ==(Identificator first, Identificator second)
        {
            if ((object)first == null || (object)second == null) return false;
            return first.Equals(second);
        }

        public static bool operator !=(Identificator first, Identificator second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Identificator;
            if (other == null) 
                return false;

            return (other.OrderIds.SequenceEqual(this.OrderIds));
        }

        public override int GetHashCode()
        {
            return (_orderIds != null ? _orderIds.GetHashCode() : 0);
        }
    }
}
