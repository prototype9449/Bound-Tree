using System;
using System.Collections.Generic;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public class Single : NodeInfo
    {
        private readonly List<Type> _validTypes = new List<Type>(new[]
        {
            typeof(PredefinedList), typeof(Answer)
        });

        public Single()
        {
            LogicLevel = 5;
        }

        internal override List<Type> ValidTypes
        {
            get { return _validTypes; }
        }
    }
}
