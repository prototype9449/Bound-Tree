using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class OpenText : NodeInfo
    {
//        private readonly List<Type> _validTypes = new List<Type>(new[]
//        {
//            typeof(PredefinedList), typeof(Answer)
//        });
//
//        protected override List<Type> ValidTypes { get { return _validTypes; } }
//
//        public OpenTextInfo()
//        {
//            LogicLevel = new LogicLevel(5);
//        }
        public OpenText(ILogicLevelProvider logicLevelProvider) : base(logicLevelProvider) { }
    }
}
