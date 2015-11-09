namespace BoundTree.NodeInfo
{
    public interface INodeInfo
    {
        int LogicLevel { get; }
        bool IsEmpty();
    }
}