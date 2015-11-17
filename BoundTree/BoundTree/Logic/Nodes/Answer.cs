using System;
using System.Collections.Generic;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public class Answer : NodeInfo
    {
        private readonly List<Type> _validTypes = new List<Type>(new[]
        {
            typeof(PredefinedList)
        });

        internal override List<Type> ValidTypes { get { return _validTypes; } }

        public Answer()
        {
            LogicLevel = 7;
        }
    }
}
