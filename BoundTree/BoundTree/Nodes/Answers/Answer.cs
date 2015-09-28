
using BoundTree.Interfaces;

namespace BoundTree.Nodes.Answers
{
    public class Answer : Node
    {
        public Answer(IBindingHandler bindingHandler, Identificator identificator) : base(identificator, bindingHandler)
        {
        }

        public Answer(Node node, IBindingHandler bindingHandler) : base(node, bindingHandler){ }

        public override Node GetNewInstance()
        {
            return new Answer(this, this.BindingHandler);
        }
    }
}
