using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class PredefinedList : NodeInfo
    {
        public PredefinedList(LogicLevelFactory logicLevelFactory) : base(logicLevelFactory) { }
    }
}
