using System;
using System.Linq;
using BoundTree.Logic;

namespace BoundTree.Helpers.TreeReconstruction
{
    public class ConnectionKindReconstruction<T> where T : class, IEquatable<T>, new()
    {
        public void Reconstruct(DoubleNode<T> doubleNode)
        {
            doubleNode.ToList()
                .Where(node => !node.MinorLeaf.IsEmpty())
                .Where(node => node.ConnectionKind != ConnectionKind.Strict).ToList()
                .ForEach(node => node.ConnectionKind = ConnectionKind.Relative);      
        }
    }
}
