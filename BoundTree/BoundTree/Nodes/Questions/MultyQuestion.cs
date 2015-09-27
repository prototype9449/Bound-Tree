using BoundTree.Interfaces;

namespace BoundTree.Nodes.Questions
{
    public class MultyQuestion : Node
    {
        public MultyQuestion(IBindingHandler bindingHandler, Identificator identificator) : base(identificator, bindingHandler)
        {
        }
    }
}
