using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class Multi : NodeInfo
    {
//        private readonly List<Type> _validTypes = new List<Type>(new[]
//        {
//            typeof(PredefinedList),typeof(OpenTextInfo),typeof(Answer), typeof(Single)
//        });
//
//        protected override List<Type> ValidTypes { get { return _validTypes; } }
//
//        public Multi()
//        {
//            LogicLevel = new LogicLevel(4);
//        }
        public Multi(ILogicLevelProvider logicLevelProvider) : base(logicLevelProvider) { }
    }
}