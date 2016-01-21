using System;
using BoundTree.Logic.LogicLevelProviders;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public abstract class NodeInfo
    {
        private readonly LogicLevelFactory _logicLevelFactory;

        protected NodeInfo(LogicLevelFactory logicLevelFactory)
        {
            _logicLevelFactory = logicLevelFactory;
        }

        public LogicLevel LogicLevel
        {
            get { return _logicLevelFactory.GetLogicLevel(this); }
        }

        public virtual bool IsEmpty()
        {
            return false;
        }

        public virtual bool CanContain(NodeInfo nodeInfo)
        {
            return _logicLevelFactory.CanFirtsContainSecond(this, nodeInfo);
        }
    }
}