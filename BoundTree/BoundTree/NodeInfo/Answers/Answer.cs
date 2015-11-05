namespace BoundTree.NodeInfo.Answers
{
    public class AnswerInfo : INodeInfo
    {
        public AnswerInfo()
        {
            LogicLevel = 5;
            Type = "Answer";
        }

        public int LogicLevel { get; private set; }
        public string Type { get; private set; }
    }
}
