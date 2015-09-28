using BoundTree.Interfaces;

namespace BoundTree.Nodes.Answers
{
    public class OpenText : Node
    {
        public OpenText( Identificator identificator, IBindingHandler bindingHandler)
            : base(identificator, bindingHandler) { }

        public OpenText(Node node, IBindingHandler bindingHandler) : base(node, bindingHandler)
        {
        }

        public override Node GetNewInstance()
        {
            return new OpenText(this, this.BindingHandler);
        }
    }
}
