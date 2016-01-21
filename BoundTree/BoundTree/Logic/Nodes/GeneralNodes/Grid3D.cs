using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class Grid3D : NodeInfo
    {
        public Grid3D(LogicLevelFactory logicLevelFactory) : base(logicLevelFactory) { }
    }
}