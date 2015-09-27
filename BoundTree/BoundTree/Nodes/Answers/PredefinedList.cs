using BoundTree.Interfaces;

namespace BoundTree.Nodes.Answers
{
    public class PredefinedList : Node
    {
        public PredefinedList(IBindingHandler bindingHandler, Identificator identificator) : base(identificator, bindingHandler)
        {
        }
    }
}
