namespace BoundTree.NodeInfo
{
    public abstract class NodeInfo : INodeInfo
    {
        public int LogicLevel { get; internal set; }
        public virtual bool IsEmpty()
        {
            return false;
        }
    }
}