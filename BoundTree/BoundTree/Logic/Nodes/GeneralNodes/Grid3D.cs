﻿using System;
using System.Collections.Generic;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class Grid3D : NodeInfo
    {
        private readonly List<Type> _validTypes = new List<Type>(new []
        {
            typeof (Grid), typeof(PredefinedList),typeof(MultiGrid),typeof(Multi),typeof(OpenTextInfo),typeof(Answer), typeof(Single)
        });

        internal override List<Type> ValidTypes { get { return _validTypes; } }

        public Grid3D()
        {
            LogicLevel = new LogicLevel(1);
        }

    }
}