using System;
using System.Collections.Generic;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public class OpenTextInfo : NodeInfo
    {
        private readonly List<Type> _validTypes = new List<Type>(new[]
        {
            typeof(PredefinedList), typeof(Answer)
        });

        internal override List<Type> ValidTypes { get { return _validTypes; } }

        public OpenTextInfo()
        {
            LogicLevel = 5;
        }
    }
}
