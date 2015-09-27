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
        public RootNode(Identificator identificator, IBindingHandler bindingHandler) : base(identificator, bindingHandler)
        {
        }

        public RootNode(Node node, IBindingHandler bindingHandler) : base(node, bindingHandler)
        {
        }
    }
}
