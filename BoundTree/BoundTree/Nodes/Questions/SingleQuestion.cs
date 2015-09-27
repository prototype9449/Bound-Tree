using BoundTree.Interfaces;

namespace BoundTree.Nodes.Questions
{
    public class SingleQuestion : Node
    {
        public SingleQuestion(IBindingHandler bindingHandler, Identificator identificator) : base(identificator, bindingHandler)
        {

        }
    }
}
