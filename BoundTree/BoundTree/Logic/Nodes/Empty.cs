using System;
using System.Collections.Generic;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public class Empty : NodeInfo
    {
//        public Empty()
//        {
//            LogicLevel = new LogicLevel();
//        }
//
//        protected override List<Type> ValidTypes
//        {
//            get { return null; }
//        }
//
//        public override bool CanContain(NodeInfo nodeInfo)
//        {
//            throw new InvalidOperationException("Empty doesn't contain other nodes");
//        }

        public override bool IsEmpty()
        {
            return true;
        }

        public Empty(ILogicLevelProvider logicLevelProvider) : base(logicLevelProvider) { }
    }
}