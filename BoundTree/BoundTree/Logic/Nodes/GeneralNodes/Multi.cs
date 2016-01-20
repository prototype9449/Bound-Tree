using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class Multi : NodeInfo
    {
        public Multi(ILogicLevelProvider logicLevelProvider) : base(logicLevelProvider) { }
    }
}