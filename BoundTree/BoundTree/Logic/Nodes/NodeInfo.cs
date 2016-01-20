using System;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public abstract class NodeInfo
    {
        private readonly ILogicLevelProvider _logicLevelProvider;

        protected NodeInfo(ILogicLevelProvider logicLevelProvider)
        {
            _logicLevelProvider = logicLevelProvider;
        }

        public LogicLevel LogicLevel
        {
            get { return _logicLevelProvider.GetLogicLevel(this); }
        }

        public virtual bool IsEmpty()
        {
            return false;
        }

        public virtual bool CanContain(NodeInfo nodeInfo)
        {
            return _logicLevelProvider.CanFirtsContainSecond(this, nodeInfo);
        }
    }
}