using System;

namespace BoundTree.NodeInfo.Questions
{
    [Serializable]
    public class GridQuestionInfo : INodeInfo
    {
        public GridQuestionInfo()
        {
            LogicLevel = 2;
            Type = "Grid";
        }

        public int LogicLevel { get; private set; }
        public string Type { get; private set; }
    }
}
