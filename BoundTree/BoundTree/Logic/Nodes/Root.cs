﻿using System;
using System.Collections.Generic;
using BoundTree.Logic.Nodes.GeneralNodes;
using Single = BoundTree.Logic.Nodes.GeneralNodes.Single;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public class Root : NodeInfo
    {
        private readonly List<Type> _validTypes = new List<Type>(new[]
        {
             typeof (Grid3D), typeof (Grid), typeof(MultiGrid),typeof(Multi),typeof(OpenTextInfo), typeof(Single)
        });

        protected override List<Type> ValidTypes { get { return _validTypes; } }


        public Root()
        {
            LogicLevel = new LogicLevel(0);
        }
    }
}
