using System;
using System.Linq;
using BoundTree.Logic.Nodes;
using Single = BoundTree.Logic.Nodes.Single;

namespace BoundTree.Helpers
{
    public class ContainHelper
    {
        private NodeInfo _nodeInfo;
        private Func<NodeInfo, NodeInfo, bool>[] Patterns =
        {
            IsMatched<Root, Grid>,
            IsMatched<Root, Grid3D>,
            IsMatched<Root, Single>,
            IsMatched<Grid3D, Grid>,
            IsMatched<Grid3D, Single>,
            IsMatched<Grid3D, PredefinedList>,
            IsMatched<Grid3D, OpenTextInfo>,
            IsMatched<OpenTextInfo, PredefinedList>,
            IsMatched<OpenTextInfo, AnswerInfo>,
            IsMatched<Grid3D, AnswerInfo>,
            IsMatched<Grid, Single>,
            IsMatched<Single, AnswerInfo>,
            IsMatched<Single, PredefinedList>,
            IsMatched<PredefinedList, AnswerInfo>
        };
        
        public ContainHelper CreateHelper(NodeInfo nodeInfo)
        {
            _nodeInfo = nodeInfo;
            return this;
        }

        public bool CanContain(NodeInfo other)
        {
            return Patterns
                .Select(func => func(_nodeInfo, other))
                .ToList()
                .Any(item => item == true);
        }

        private static bool IsMatched<T1, T2>(NodeInfo firtsNode, NodeInfo secondNode)
        {
            return firtsNode is T1 && secondNode is T2;
        }
    }
}