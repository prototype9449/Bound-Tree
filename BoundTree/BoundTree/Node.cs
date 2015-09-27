using System.Collections.Generic;
using BoundTree.Helpers;
using BoundTree.Interfaces;

namespace BoundTree
{
    abstract public class Node
    {
        private BindingHelper _bindingHelper = new BindingHelper();
        private IBindingHandler _bindingHandler;
        public Identificator Identificator { get; private set; }
        public List<Node> Nodes { get; private set; }
        protected Node(Identificator identificator, IBindingHandler bindingHandler)
        {
            _bindingHandler = bindingHandler;
            Identificator = identificator;
            Nodes = new List<Node>();
        }

        protected Node(Node node, IBindingHandler bindingHandler)
        {
            Identificator = node.Identificator;
            _bindingHandler = bindingHandler;
        }

        public bool BindWith(Node otherNode)
        {
            if (_bindingHelper.Bind(this, otherNode))
            {
                _bindingHandler.HandleBinding(this, otherNode);
                return true;
            }

            return false;
        }

        public bool Add(Node otherNode)
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

        public Node GetNodeByIdentificator(Identificator identificator)
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
