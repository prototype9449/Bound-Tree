using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class Single : NodeInfo
    {
        public Single(ILogicLevelProvider logicLevelProvider) : base(logicLevelProvider) { }
    }
}
