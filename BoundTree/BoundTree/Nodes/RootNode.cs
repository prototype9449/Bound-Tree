using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundTree.Interfaces;

namespace BoundTree.Nodes
{
    public class RootNode : Node
    {
        public RootNode(int id, IBindingHandler bindingHandler) : base(id, bindingHandler)
        {
        }

        public RootNode(Node node, IBindingHandler bindingHandler) : base(node, bindingHandler)
        {
        }

        public override Node GetNewInstance()
        {
            return new RootNode(this, this.BindingHandler);
        }
    }
}
