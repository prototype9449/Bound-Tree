namespace BoundTree.NodeInfo
{
    public class EmptyNodeInfo : INodeInfo
    {
        public EmptyNodeInfo()
        {
            LogicLevel = -1;
            Type = "Empty";
        }

        public int LogicLevel { get; private set; }
        public string Type { get; private set; }
    }
}