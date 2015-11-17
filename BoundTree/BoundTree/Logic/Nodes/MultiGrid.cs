using System;
using System.Collections.Generic;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public class MultiGrid : NodeInfo
    {
        private readonly List<Type> _validTypes = new List<Type>(new[]
        {
            typeof (Grid), typeof(PredefinedList),typeof(Multi),typeof(OpenTextInfo),typeof(Answer), typeof(Single)
        });

        internal override List<Type> ValidTypes { get { return _validTypes; } }

        public MultiGrid()
        {
            LogicLevel = 2;
        }
    }
}