using BoundTree.Interfaces;

namespace BoundTree.Nodes.Answers
{
    public class OpenText : Node
    {
        public OpenText(IBindingHandler tree, Identificator identificator) : base(identificator, tree)
        {
        }
    }
}
