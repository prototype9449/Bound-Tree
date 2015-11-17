using System;
using System.Collections.Generic;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public class Root : NodeInfo
    {
        private readonly List<Type> _validTypes = new List<Type>(new[]
        {
             typeof (Grid3D), typeof (Grid), typeof(PredefinedList),typeof(MultiGrid),typeof(Multi),typeof(OpenTextInfo),typeof(Answer), typeof(Single)
        });

        internal override List<Type> ValidTypes { get { return _validTypes; } }


        public Root()
        {
            LogicLevel = 0;
        }
    }
}
