using System;
using System.Collections.Generic;
using BoundTree.Interfaces;

namespace BoundTree.Nodes.Questions
{
    [Serializable]
    public class SingleQuestion : Node
    {
        public SingleQuestion(int id, IBindingHandler bindingHandler, IList<Node> nodes) : base(id, bindingHandler, nodes) { }

        public SingleQuestion(int id, IBindingHandler bindingHandler) : base(id, bindingHandler) { }

        public SingleQuestion(Node node, IBindingHandler bindingHandler) : base(node, bindingHandler) { }

        public override Node GetNewInstance()
        {
            return new SingleQuestion(this, this.BindingHandler);
        }
    }
}
