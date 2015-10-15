using System;
using System.Collections.Generic;
using BoundTree.Interfaces;

namespace BoundTree.Helpers
{
    [Serializable]
    public class BindingHandler<T> : IBindingHandler<T>
    {
        private readonly List<KeyValuePair<T, T>> _boundNodes = new List<KeyValuePair<T, T>>();

        public List<KeyValuePair<T, T>> BoundNodes
        {
            get { return _boundNodes; }
        }

        public void HandleBinding(Node<T> mainNode, Node<T> minorNode)
        {
            BoundNodes.Add(new KeyValuePair<T, T>(mainNode.Id, minorNode.Id));
        }

        public void RemoveConnection(T mainId)
        {
            _boundNodes.RemoveAll(pair => pair.Key.Equals(mainId));
        }
    }
}
