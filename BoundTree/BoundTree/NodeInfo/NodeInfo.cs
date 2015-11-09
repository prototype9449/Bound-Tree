using System;

namespace BoundTree.NodeInfo
{
    [Serializable]
    public abstract class NodeInfo : INodeInfo
    {
        public int LogicLevel { get; internal set; }
        public virtual bool IsEmpty()
        {
            return false;
        }
    }
}