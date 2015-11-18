using System;
using System.Collections.Generic;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class PredefinedList : NodeInfo
    {
        private readonly List<Type> _validTypes = new List<Type>(new[]
        {
            typeof(PredefinedList), typeof(Answer)
        });

        internal override List<Type> ValidTypes { get { return _validTypes; } }

        public PredefinedList()
        {
            LogicLevel = 6;
        }
    }
}
