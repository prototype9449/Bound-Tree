using System;
using System.Collections.Generic;
using BoundTree.Interfaces;

namespace BoundTree.Helpers
{
    public class BindingHandler : IBindingHandler
    {
        private readonly List<KeyValuePair<int, int>> _boundNodes =
           new List<KeyValuePair<int, int>>();

        public List<KeyValuePair<int, int>> BoundNodes
        {
            get { return _boundNodes; }
        }

        public void HandleBinding(Node mainNode, Node minorNode)
        {
            BoundNodes.Add(new KeyValuePair<int, int>(mainNode.Id, minorNode.Id));
        }
    }

    
}
