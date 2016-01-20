using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class MultiGrid : NodeInfo
    {
        public MultiGrid(ILogicLevelProvider logicLevelProvider) : base(logicLevelProvider) { }
    }
}