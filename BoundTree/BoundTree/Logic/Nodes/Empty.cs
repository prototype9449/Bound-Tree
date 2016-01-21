using System;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public class Empty : NodeInfo
    {
        public override bool IsEmpty()
        {
            return true;
        }

        public Empty(LogicLevelFactory logicLevelFactory) : base(logicLevelFactory) { }
    }
}