using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class Answer : NodeInfo
    {
//        private readonly List<Type> _validTypes = new List<Type>(new[]
//        {
//            typeof(PredefinedList)
//        });
//
//        protected override List<Type> ValidTypes { get { return _validTypes; } }
//
//        public Answer()
//        {
//            LogicLevel = new LogicLevel(7);
//        }
        public Answer(ILogicLevelProvider logicLevelProvider) : base(logicLevelProvider) { }
    }
}
