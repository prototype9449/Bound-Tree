using System;
using System.Collections.Generic;
using BoundTree.Helpers;
using BoundTree.Interfaces;
using BoundTree.Nodes;

namespace BoundTree
{
    [Serializable]
    public class Node
    {
        private readonly BindingHelper _bindingHelper = new BindingHelper();
        private readonly IBindingHandler _bindingHandler;
        public int Id { get; private set; }
        public List<Node> Nodes { get; internal set; }
        public INodeInfo NodeInfo { get; set; }

        
        public IBindingHandler BindingHandler
        {
            get { return _bindingHandler; }
        }

        public Node(int id, INodeInfo nodeInfo, IBindingHandler bindingHandler, IList<Node> nodes)
        {
            Nodes = new List<Node>(nodes);
            NodeInfo = nodeInfo;
            _bindingHandler = bindingHandler;
            Id = id;
        }

        public Node(int id, INodeInfo nodeInfo, IBindingHandler bindingHandler)
            : this(id, nodeInfo, bindingHandler, new List<Node>())
        { }

        public Node(Node node, INodeInfo nodeInfo, IBindingHandler bindingHandler)
            : this(node.Id, nodeInfo, bindingHandler, new List<Node>())
        { }

        public bool BindWith(Node otherNode)
        {
            if (_bindingHelper.Bind(this.NodeInfo, otherNode.NodeInfo))
            {
                BindingHandler.HandleBinding(this, otherNode);
                return true;
            }

            return false;
        }
    }
}
