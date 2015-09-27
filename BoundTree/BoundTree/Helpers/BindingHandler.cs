using System;
using System.Collections.Generic;
using BoundTree.Interfaces;

namespace BoundTree.Helpers
{
    public class BindingHandler : IBindingHandler
    {
        private readonly List<KeyValuePair<Identificator, Identificator>> _boundNodes =
           new List<KeyValuePair<Identificator, Identificator>>();

        public List<KeyValuePair<Identificator, Identificator>> BoundNodes
        {
            get { return _boundNodes; }
        }

        public void HandleBinding(Node mainNode, Node minorNode)
        {
            BoundNodes.Add(new KeyValuePair<Identificator, Identificator>(mainNode.Identificator, minorNode.Identificator));
        }
    }

    
}
