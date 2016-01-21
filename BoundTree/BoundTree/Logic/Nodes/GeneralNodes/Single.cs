using System;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class Single : NodeInfo
    {
        public Single(LogicLevelFactory logicLevelFactory) : base(logicLevelFactory) { }
    }
}
