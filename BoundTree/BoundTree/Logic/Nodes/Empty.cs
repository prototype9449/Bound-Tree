using System;
using System.Collections.Generic;

namespace BoundTree.Logic.Nodes
{
    public class Empty : NodeInfo
    {
        public Empty()
        {
            LogicLevel = new LogicLevel();
        }

        internal override List<Type> ValidTypes
        {
            get { return null; }
        }

        public override bool CanContain(NodeInfo nodeInfo)
        {
            throw new InvalidOperationException("Empty doesn't contain other nodes");
        }

        public override bool IsEmpty()
        {
            return true;
        }
    }
}