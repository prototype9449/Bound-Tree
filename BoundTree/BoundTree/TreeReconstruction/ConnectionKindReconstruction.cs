using System;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Logic;

namespace BoundTree.TreeReconstruction
{
    public class ConnectionKindReconstruction<T> where T : class, IEquatable<T>, new()
    {
        public void Reconstruct(DoubleNode<T> doubleNode)
        {
            Contract.Requires(doubleNode != null);

            foreach (var node in doubleNode.ToList().Where(node => node.ConnectionKind == ConnectionKind.None))
            {
                if (node.ToList().Exists(item => item.ConnectionKind != ConnectionKind.None))
                {
                    node.ConnectionKind = ConnectionKind.Relative;
                }
            }
        }
    }
}
