using BoundTree.Interfaces;

namespace BoundTree.Nodes
{
    public class EmptyNode : Node
    {
        public EmptyNode(int id, IBindingHandler bindingHandler) : base(id, bindingHandler)
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