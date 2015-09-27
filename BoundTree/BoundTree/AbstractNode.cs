using System.Collections.Generic;
using BoundTree.Helpers;
using BoundTree.Interfaces;

namespace BoundTree
{
    abstract public class AbstractNode : INode
    {
        private BindingHelper _bindingHelper = new BindingHelper();
        private Tree _tree;
        public Identificator Identificator { get; private set; }
        public List<INode> Nodes { get; private set; }
        protected AbstractNode(Tree tree, Identificator identificator)
        {
            _tree = tree;
            Identificator = identificator;
            Nodes = new List<INode>();
        }
        
        public bool BindWith(INode otherNode)
        {
            if (_bindingHelper.Bind(this, otherNode))
            {
                _tree.HandleBinding(this, otherNode);
                return true;
            }

            return false;
        }

        public bool Add(INode otherNode)
        {
            foreach (var node in Nodes)
            {
                if (otherNode.Identificator == null) continue;

                if (node.Identificator.NeedToInsert(otherNode.Identificator))
                {
                    return node.Add(otherNode);
                }
            }

            Nodes.Add(otherNode);
            return true;
        }

        public INode GetNodeByIdentificator(Identificator identificator)
        {
            if (identificator == this.Identificator)
                return this;

            foreach (var node in Nodes)
            {
                if (node.Identificator.NeedToInsert(identificator))
                {
                    return node.GetNodeByIdentificator(identificator);
                }
                if (node.Identificator == identificator) return node;
            }

            return null;
        }
    }
}
