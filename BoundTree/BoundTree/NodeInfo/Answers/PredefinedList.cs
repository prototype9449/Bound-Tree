namespace BoundTree.NodeInfo.Answers
{
    public class PredefinedListInfo : INodeInfo
    {
        public PredefinedListInfo()
        {
            LogicLevel = 4;
            Type = "Predefined";
        }

        public int LogicLevel { get; private set; }
        public string Type { get; private set; }
    }
}
