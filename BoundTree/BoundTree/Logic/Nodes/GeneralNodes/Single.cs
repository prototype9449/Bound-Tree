using System;
using System.Collections.Generic;

namespace BoundTree.Logic.Nodes.GeneralNodes
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
            LogicLevel = new LogicLevel(5);
        }

        protected override List<Type> ValidTypes
        {
            get { return _validTypes; }
        }
    }
}
