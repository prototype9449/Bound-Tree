using BoundTree.Interfaces;

namespace BoundTree.Nodes.Answers
{
    public class PredefinedList : Node
    {
        public PredefinedList(IBindingHandler bindingHandler, Identificator identificator) : base(identificator, bindingHandler)
        {
        }

        public PredefinedList(Node node, IBindingHandler bindingHandler)
            : base(node, bindingHandler)
        {
        }
        public override Node GetNewInstance()
        {
            return new PredefinedList(this, this.BindingHandler);
        }
    }
}
