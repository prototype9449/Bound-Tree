using System;
using BoundTree.NodeInfo;
using BoundTree.NodeInfo.Answers;
using BoundTree.NodeInfo.Questions;

namespace BoundTree.Helpers
{
    [Serializable]
    public class BindingHelper
    {
        private Func<INodeInfo, INodeInfo, bool>[] Patterns =
        {
            IsMatched<GridQuestionInfo, GridQuestionInfo>,
            IsMatched<SingleQuestionInfo, SingleQuestionInfo>,
            IsMatched<OpenTextInfo, OpenTextInfo>
        };

        public bool Bind(INodeInfo firtsNode, INodeInfo secondNode)
        {
            return true;
        }

        private static bool IsMatched<T1, T2>(INodeInfo firtsNode, INodeInfo secondNode)
        {
            return firtsNode is T1 && secondNode is T2;
        }
    }
}
