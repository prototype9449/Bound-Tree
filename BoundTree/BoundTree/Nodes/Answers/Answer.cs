
using BoundTree.Interfaces;

namespace BoundTree.Nodes.Answers
{
    public class Answer : Node
    {
        public Answer(IBindingHandler bindingHandler, Identificator identificator) : base(identificator, bindingHandler)
        {
        }
    }
}
