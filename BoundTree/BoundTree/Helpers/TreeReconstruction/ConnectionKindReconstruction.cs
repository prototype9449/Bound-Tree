using System;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Logic;

namespace BoundTree.Helpers.TreeReconstruction
{
    public class ConnectionKindReconstruction<T> where T : class, IEquatable<T>, new()
    {
        public void Reconstruct(DoubleNode<T> doubleNode)
        {
            Contract.Requires(doubleNode != null);

            doubleNode.ToList()
                .Where(node => !node.MinorLeaf.IsEmpty())
                .Where(node => node.ConnectionKind != ConnectionKind.Strict).ToList()
                .ForEach(node => node.ConnectionKind = ConnectionKind.Relative);      
        }
    }
}
