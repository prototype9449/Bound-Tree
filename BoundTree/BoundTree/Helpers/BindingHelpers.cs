using System;
using System.Linq;
using BoundTree.Nodes;
using BoundTree.Nodes.Answers;
using BoundTree.Nodes.Questions;

namespace BoundTree.Helpers
{
    public class BindingHelper
    {
        private Func<Node, Node, bool>[] Patterns =
        {
            IsMatched<GridQuestion, GridQuestion>,
            IsMatched<SingleQuestion, SingleQuestion>,
            IsMatched<OpenText, OpenText>
        };

        public bool Bind(Node firtsNode, Node secondNode)
        {
            return Patterns.Select(func => func(firtsNode, secondNode)).Any(result => result == true);
        }

        private static bool IsMatched<T1, T2>(Node firtsNode, Node secondNode)
        {
            return firtsNode is T1 && secondNode is T2;
        }
    }
}
