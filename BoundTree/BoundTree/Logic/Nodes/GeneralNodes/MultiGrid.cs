using System;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class MultiGrid : NodeInfo
    {
        public MultiGrid(LogicLevelFactory logicLevelFactory) : base(logicLevelFactory) { }
    }
}