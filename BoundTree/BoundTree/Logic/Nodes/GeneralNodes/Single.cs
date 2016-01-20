using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class Single : NodeInfo
    {
//        private readonly List<Type> _validTypes = new List<Type>(new[]
//        {
//            typeof(PredefinedList), typeof(Answer)
//        });
//
//        public Single()
//        {
//            LogicLevel = new LogicLevel(5);
//        }
//
//        protected override List<Type> ValidTypes
//        {
//            get { return _validTypes; }
//        }
        public Single(ILogicLevelProvider logicLevelProvider) : base(logicLevelProvider) { }
    }
}
