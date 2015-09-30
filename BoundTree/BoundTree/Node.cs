using System;
using System.Collections.Generic;
using BoundTree.Helpers;
using BoundTree.Interfaces;

namespace BoundTree
{
    [Serializable]
    public class Node
    {
        private readonly BindingHelper _bindingHelper = new BindingHelper();
        private readonly IBindingHandler _bindingHandler;
        public int Id { get; private set; }
        public List<Node> Nodes { get; internal set; }
        
        public IBindingHandler BindingHandler
        {
            get { return _bindingHandler; }
        }

        protected Node(int id, IBindingHandler bindingHandler, IList<Node> nodes)
        {
            Nodes = new List<Node>(nodes);
            _bindingHandler = bindingHandler;
            Id = id;
        }

        protected Node(int id, IBindingHandler bindingHandler) : this(id, bindingHandler, new List<Node>())
        { }

        protected Node(Node node, IBindingHandler bindingHandler) : this(node.Id, bindingHandler, new List<Node>())
        { }

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
