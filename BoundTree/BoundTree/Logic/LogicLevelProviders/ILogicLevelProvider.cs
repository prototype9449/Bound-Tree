using BoundTree.Logic.Nodes;

namespace BoundTree.Logic.LogicLevelProviders
{
    public interface ILogicLevelProvider
    {
        LogicLevel GetLogicLevel(NodeInfo nodeInfo);
        bool CanFirtsContainSecond(NodeInfo first, NodeInfo second);
    }
}