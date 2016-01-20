using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class Grid : NodeInfo
    {
        public Grid(ILogicLevelProvider logicLevelProvider) : base(logicLevelProvider) { }
    }
}
