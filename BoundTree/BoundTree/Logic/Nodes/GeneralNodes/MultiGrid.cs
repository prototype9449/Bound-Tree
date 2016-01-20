using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class MultiGrid : NodeInfo
    {
//        private readonly List<Type> _validTypes = new List<Type>(new[]
//        {
//            typeof (Grid),typeof(Multi),typeof(OpenTextInfo), typeof(Single)
//        });
//
//        protected override List<Type> ValidTypes { get { return _validTypes; } }
//
//        public MultiGrid()
//        {
//            LogicLevel = new LogicLevel(2);
//        }
        public MultiGrid(ILogicLevelProvider logicLevelProvider) : base(logicLevelProvider) { }
    }
}