using BoundTree.Interfaces;

namespace BoundTree.Nodes
{
    public class EmptyNode : Node
    {
        public EmptyNode(Identificator identificator, IBindingHandler bindingHandler) : base(identificator, bindingHandler)
        {
        }

        public EmptyNode(Node node, IBindingHandler bindingHandler) : base(node, bindingHandler)
        {
        }

        public override Node GetNewInstance()
        {
            return new EmptyNode(this, this.BindingHandler);
        }
    }
}