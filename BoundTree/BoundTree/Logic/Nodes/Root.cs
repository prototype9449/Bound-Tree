using System;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public class Root : NodeInfo
    {
        public Root(LogicLevelFactory logicLevelFactory) : base(logicLevelFactory) { }
    }
}
