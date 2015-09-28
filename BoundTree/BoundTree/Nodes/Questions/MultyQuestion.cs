using BoundTree.Interfaces;

namespace BoundTree.Nodes.Questions
{
    public class MultyQuestion : Node
    {
        public MultyQuestion(IBindingHandler bindingHandler, Identificator identificator) : base(identificator, bindingHandler) { }

        public MultyQuestion(Node node, IBindingHandler bindingHandler) : base(node, bindingHandler) { }

        public override Node GetNewInstance()
        {
            return new MultyQuestion(this, this.BindingHandler);
        }
    }
}
