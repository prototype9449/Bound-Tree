using System.Collections.Generic;
using BoundTree.Logic;

namespace BoundTree.Interfaces
{
    public interface IBindingHandler<T> where T : new()
    {
        bool HandleBinding(SingleNode<T> mainSingleNode, SingleNode<T> minorSingleNode);
        bool RemoveConnection(T mainId);
        List<KeyValuePair<T, T>> Connections { get; }
    }
}
