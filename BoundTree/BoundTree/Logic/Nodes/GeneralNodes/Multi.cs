using System;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class Multi : NodeInfo
    {
        public Multi(LogicLevelFactory logicLevelFactory) : base(logicLevelFactory) { }
    }
}