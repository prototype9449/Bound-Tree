using System;
using System.Linq;
using BoundTree.Nodes;
using BoundTree.Nodes.Answers;
using BoundTree.Nodes.Questions;

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
            return Patterns.Select(func => func(firtsNode, secondNode)).Any(result => result == true);
        }

        private static bool IsMatched<T1, T2>(INodeInfo firtsNode, INodeInfo secondNode)
        {
            return firtsNode is T1 && secondNode is T2;
        }
    }
}
