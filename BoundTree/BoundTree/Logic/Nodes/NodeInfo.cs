using System;
using System.Collections.Generic;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public abstract class NodeInfo 
    {
        internal abstract List<Type> ValidTypes { get; }

        public LogicLevel LogicLevel { get; internal set; }

        public virtual bool IsEmpty()
        {
            return false;
        }

        public virtual bool CanContain(NodeInfo nodeInfo)
        {
            return ValidTypes.Contains(nodeInfo.GetType());
        }
    }
}