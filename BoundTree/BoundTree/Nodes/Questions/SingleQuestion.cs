using BoundTree.Interfaces;

namespace BoundTree.Nodes.Questions
{
    public class SingleQuestion : Node
    {
        public SingleQuestion(IBindingHandler bindingHandler, Identificator identificator) : base(identificator, bindingHandler) { }

        public SingleQuestion(Node node, IBindingHandler bindingHandler) : base(node, bindingHandler) { }

        public override Node GetNewInstance()
        {
            return new SingleQuestion(this, this.BindingHandler);
        }
    }
}
