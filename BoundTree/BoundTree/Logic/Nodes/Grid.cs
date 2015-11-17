using System;
using System.Collections.Generic;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public class Grid : NodeInfo
    {
        private readonly List<Type> _validTypes = new List<Type>(new[]
        {
            typeof(PredefinedList),typeof(Multi),typeof(OpenTextInfo),typeof(Answer), typeof(Single)
        });

        internal override List<Type> ValidTypes { get { return _validTypes; } }

        public Grid()
        {
            this.LogicLevel = 3;
        }
    }
}
