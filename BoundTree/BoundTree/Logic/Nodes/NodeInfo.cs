using System;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public abstract class NodeInfo 
    {
        public int LogicLevel { get; internal set; }

        public virtual bool IsEmpty()
        {
            return false;
        }
    }
}