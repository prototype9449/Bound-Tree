﻿using System.Collections.Generic;
using BoundTree.Logic;

namespace BoundTree.Interfaces
{
    public interface IBindingHandler<T> where T : new()
    {
        void HandleBinding(SingleNode<T> mainSingleNode, SingleNode<T> minorSingleNode);
        void RemoveConnection(T mainId);
        List<KeyValuePair<T, T>> BoundNodes { get; }
    }
}
