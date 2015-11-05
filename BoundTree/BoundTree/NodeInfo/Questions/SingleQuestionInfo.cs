using System;

namespace BoundTree.NodeInfo.Questions
{
    [Serializable]
    public class SingleQuestionInfo : INodeInfo
    {
        public SingleQuestionInfo()
        {
            LogicLevel = 3;
            Type = "Single";
        }

        public int LogicLevel { get; private set; }
        public string Type { get; private set; }
    }
}
