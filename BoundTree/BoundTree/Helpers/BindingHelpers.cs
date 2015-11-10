using System;
using BoundTree.Logic.Nodes;
using Single = System.Single;

namespace BoundTree.Helpers
{
    [Serializable]
    public class BindingHelper
    {
        private Func<NodeInfo, NodeInfo, bool>[] Patterns =
        {
            IsMatched<Grid, Grid>,
            IsMatched<Single, Single>,
            IsMatched<OpenTextInfo, OpenTextInfo>
        };

        public bool Bind(NodeInfo firtsNode, NodeInfo secondNode)
        {
            return true;
        }

        private static bool IsMatched<T1, T2>(NodeInfo firtsNode, NodeInfo secondNode)
        {
            return firtsNode is T1 && secondNode is T2;
        }
    }
}
