using System.Collections.Generic;

namespace BoundTree.Interfaces
{
    public interface INode
    {
        Identificator Identificator { get; }
        List<INode> Nodes { get; }
        bool BindWith(INode otherNode);
        bool Add(INode otherNode);
        INode GetNodeByIdentificator(Identificator identificator);
    }
}