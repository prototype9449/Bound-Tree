using BoundTree.Interfaces;

namespace BoundTree.Nodes.Questions
{
    public class GridQuestion : Node
    {
        public GridQuestion(IBindingHandler bindingHandler, Identificator identificator) : base(identificator, bindingHandler)
        {
        }
    }
}
