using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundTree.Logic;
using BoundTree.Logic.Nodes;

namespace BoundTree.Helpers.TreeReconstruction
{
    public class ConnectionKindReconstruction<T> where T : class, IEquatable<T>, new()
    {
        private BindingHandler<T> _bindingHandler;

        public ConnectionKindReconstruction(BindingHandler<T> bindingHandler)
        {
            _bindingHandler = bindingHandler;
        }

        public void Reconstruct(DoubleNode<T> doubleNode)
        {
            var connections = _bindingHandler.Connections;

            doubleNode.ToList()
                .Where(node => !node.MinorLeaf.IsEmpty())
                .Where(node => node.ConnectionKind != ConnectionKind.Strict).ToList()
                .ForEach(node => node.ConnectionKind = ConnectionKind.Relative);      
        }
    }
}
