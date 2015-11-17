using System;
using System.Collections.Generic;
using System.Linq;

namespace BoundTree.Logic.Nodes
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
            LogicLevel = 1;
        }

    }
}