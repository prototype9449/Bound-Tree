using BoundTree.Interfaces;

namespace BoundTree.Nodes.Questions
{
    public class GridQuestion : Node
    {
        public GridQuestion(IBindingHandler bindingHandler, Identificator identificator) : base(identificator, bindingHandler) { }

        public GridQuestion(Node node, IBindingHandler bindingHandler) : base(node, bindingHandler) { }
        

        public override Node GetNewInstance()
        {
            return new GridQuestion(this, this.BindingHandler);
        }
    }
}
