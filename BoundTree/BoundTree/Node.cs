using System;
using System.Collections.Generic;
using BoundTree.Helpers;
using BoundTree.Interfaces;
using BoundTree.Nodes;

namespace BoundTree
{
    [Serializable]
    public class Node<T>
    {
        private readonly BindingHelper _bindingHelper = new BindingHelper();
        private readonly IBindingHandler<T> _bindingHandler;
        public T Id { get; private set; }
        public List<Node<T>> Nodes { get; internal set; }
        public INodeInfo NodeInfo { get; set; }
        internal int Deep { get; private set; }

        public IBindingHandler<T> BindingHandler
        {
            get { return _bindingHandler; }
        }

        public Node(T id, INodeInfo nodeInfo, IBindingHandler<T> bindingHandler, IList<Node<T>> nodes)
        {
            Deep = -1;
            Nodes = new List<Node<T>>(nodes);
            NodeInfo = nodeInfo;
            _bindingHandler = bindingHandler;
            Id = id;
        }

        public Node(T id, INodeInfo nodeInfo, IBindingHandler<T> bindingHandler)
            : this(id, nodeInfo, bindingHandler, new List<Node<T>>())
        { }

        public Node(Node<T> node, INodeInfo nodeInfo, IBindingHandler<T> bindingHandler)
            : this(node.Id, nodeInfo, bindingHandler, new List<Node<T>>())
        { }

        public bool BindWith(Node<T> otherNode)
        {
            if (_bindingHelper.Bind(this.NodeInfo, otherNode.NodeInfo))
            {
                BindingHandler.HandleBinding(this, otherNode);
                return true;
            }

            return false;
        }

        internal void SetDeep(int initialDeep)
        {
            Deep = initialDeep + 1;
            Nodes.ForEach(node => node.SetDeep(Deep));
        }

        internal void IncreaseDeep()
        {
            SetDeep(Deep);
        }
    }
}
