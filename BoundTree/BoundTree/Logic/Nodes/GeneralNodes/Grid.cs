using System;
using System.Collections.Generic;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class Grid : NodeInfo
    {
        private readonly List<Type> _validTypes = new List<Type>(new[]
        {
            typeof(Multi),typeof(OpenTextInfo), typeof(Single)
        });

        internal override List<Type> ValidTypes { get { return _validTypes; } }

        public Grid()
        {
            this.LogicLevel = new LogicLevel(3);
        }
    }
}
