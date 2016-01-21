using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;
using BoundTree.Logic.Nodes.GeneralNodes;
using Single = BoundTree.Logic.Nodes.GeneralNodes.Single;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public class Root : NodeInfo
    {
        public Root(LogicLevelFactory logicLevelFactory) : base(logicLevelFactory) { }
    }
}
