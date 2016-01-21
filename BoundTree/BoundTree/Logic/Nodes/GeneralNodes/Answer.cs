using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class Answer : NodeInfo
    {
        public Answer(LogicLevelFactory logicLevelFactory) : base(logicLevelFactory) { }
    }
}
