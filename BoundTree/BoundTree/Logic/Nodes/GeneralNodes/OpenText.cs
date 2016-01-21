﻿using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class OpenText : NodeInfo
    {
        public OpenText(LogicLevelFactory logicLevelFactory) : base(logicLevelFactory) { }
    }
}
