using System.Collections.Generic;
using BoundTree.Interfaces;

namespace BoundTree.Nodes.Answers
{
    public class OpenText : Node
    {
        public OpenText(int id, IBindingHandler bindingHandler, IList<Node> nodes) : base(id, bindingHandler, nodes) 
        { }

        public OpenText(int id, IBindingHandler bindingHandler) : base(id, bindingHandler) 
        { }

        public OpenText(Node node, IBindingHandler bindingHandler) : base(node, bindingHandler)
        { }

        public override Node GetNewInstance()
        {
            return new OpenText(this, this.BindingHandler);
        }
    }
}
