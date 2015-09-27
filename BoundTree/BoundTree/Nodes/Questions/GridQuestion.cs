using BoundTree.Interfaces;

namespace BoundTree.Nodes.Questions
{
    public class GridQuestion : Node
    {
        public GridQuestion(IBindingHandler bindingHandler, Identificator identificator) : base(identificator, bindingHandler) { }

        public GridQuestion(Node node, IBindingHandler bindingHandler) : base(node, bindingHandler) { }

        public override Node GetNodeByIdentificator(Identificator identificator)
        {
            var node = base.GetNodeByIdentificator(identificator);
            return new GridQuestion(node, null);
        }

        public override Node GetNewInstance(Node node, IBindingHandler bindingHandler)
        {
            return new GridQuestion(node, bindingHandler);
        }
    }
}
