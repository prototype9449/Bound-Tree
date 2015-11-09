using System;
using System.Collections.Generic;
using BoundTree.Interfaces;
using BoundTree.Logic;

namespace BoundTree.Helpers
{
    [Serializable]
    public class BindingHandler<T> : IBindingHandler<T> where T : new()
    {
        private readonly List<KeyValuePair<T, T>> _boundNodes = new List<KeyValuePair<T, T>>();

        public List<KeyValuePair<T, T>> BoundNodes
        {
            get { return _boundNodes; }
        }

        public void HandleBinding(SingleNode<T> mainSingleNode, SingleNode<T> minorSingleNode)
        {
            BoundNodes.Add(new KeyValuePair<T, T>(mainSingleNode.Node.Id, minorSingleNode.Node.Id));
        }

        public void RemoveConnection(T mainId)
        {
            _boundNodes.RemoveAll(pair => pair.Key.Equals(mainId));
        }
    }
}
