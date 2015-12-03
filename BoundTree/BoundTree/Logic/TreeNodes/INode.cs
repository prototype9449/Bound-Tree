using System;
using System.Collections.Generic;

namespace BoundTree.Logic.TreeNodes
{
    public interface INode<T> where T : new()
    {
        LogicLevel LogicLevel { get; }
        int Depth { get; set; }
        T Id { get; }
        Type NodeType { get; }
    }
}