using System;
using System.Collections.Generic;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class MultiGrid : NodeInfo
    {
        private readonly List<Type> _validTypes = new List<Type>(new[]
        {
            typeof (Grid),typeof(Multi),typeof(OpenTextInfo), typeof(Single)
        });

        protected override List<Type> ValidTypes { get { return _validTypes; } }

        public MultiGrid()
        {
            LogicLevel = new LogicLevel(2);
        }
    }
}