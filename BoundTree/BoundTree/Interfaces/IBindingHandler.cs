using System;
using System.Collections.Generic;

namespace BoundTree.Interfaces
{
    public interface IBindingHandler<T>
    {
        void HandleBinding(Node<T> mainNode, Node<T> minorNode);
        void RemoveConnection(T mainId);
        List<KeyValuePair<T,T>> BoundNodes { get; }
    }
}
