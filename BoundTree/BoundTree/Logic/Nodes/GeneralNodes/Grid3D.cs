using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes.GeneralNodes
{
    [Serializable]
    public class Grid3D : NodeInfo
    {
//        private readonly List<Type> _validTypes = new List<Type>(new []
//        {
//            typeof (Grid), typeof(MultiGrid),typeof(Multi),typeof(OpenTextInfo), typeof(Single)
//        });
//
//        protected override List<Type> ValidTypes { get { return _validTypes; } }
//
//        public Grid3D()
//        {
//            LogicLevel = new LogicLevel(1);
//        }

        public Grid3D(ILogicLevelProvider logicLevelProvider) : base(logicLevelProvider) { }
    }
}