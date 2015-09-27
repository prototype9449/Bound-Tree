using System;
using System.Linq;
using BoundTree.Interfaces;
using BoundTree.Nodes;

namespace BoundTree.Helpers
{
    public class BindingHelper
    {
        private Func<INode, INode, bool>[] Patterns =
        {
            IsMatched<GridQuestion, GridQuestion>,
            IsMatched<SingleQuestion, SingleQuestion>,
            IsMatched<OpenText, OpenText>
        };

        public bool Bind(INode firtsNode, INode secondNode)
        {
            return Patterns.Select(func => func(firtsNode, secondNode)).Any(result => result == true);
        }

        private static bool IsMatched<T1, T2>(INode firtsNode, INode secondNode)
        {
            return firtsNode is T1 && secondNode is T2;
        }
    }
}
