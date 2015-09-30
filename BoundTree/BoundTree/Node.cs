using System.Collections.Generic;
using System.Linq;
using BoundTree.Helpers;
using BoundTree.Interfaces;

namespace BoundTree
{
    abstract public class Node
    {
        private readonly BindingHelper _bindingHelper = new BindingHelper();
        private readonly IBindingHandler _bindingHandler;
        public int Id { get; private set; }
        public List<Node> Nodes { get; internal set; }
        
        public IBindingHandler BindingHandler
        {
            get { return _bindingHandler; }
        }

        protected Node(int id, IBindingHandler bindingHandler)
        {
            _bindingHandler = bindingHandler;
            Id = id;
            Nodes = new List<Node>();
        }

        protected Node(Node node, IBindingHandler bindingHandler)
        {
            _bindingHandler = bindingHandler;
            Id = node.Id;
            Nodes = new List<Node>();
        }

        public bool BindWith(Node otherNode)
        {
            if (_bindingHelper.Bind(this, otherNode))
            {
                BindingHandler.HandleBinding(this, otherNode);
                return true;
            }

            return false;
        }

        public abstract Node GetNewInstance();
    }
}
