using System;

namespace BoundTree.NodeInfo.Questions
{
    [Serializable]
    public class MultyQuestionInfo : INodeInfo
    {
        public MultyQuestionInfo()
        {
            LogicLevel = 3;
            Type = "Multy";
        }

        public int LogicLevel { get; private set; }
        public string Type { get; private set; }
    }
}
