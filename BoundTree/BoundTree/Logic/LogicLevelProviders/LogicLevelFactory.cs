using System;
using System.Diagnostics.Contracts;
using BoundTree.Logic.Nodes;

namespace BoundTree.Logic.LogicLevelProviders
{
    [Serializable]
    public class LogicLevelFactory
    {
        private ILogicLevelProvider _logicLevelProvider;

        public LogicLevelFactory(ILogicLevelProvider logicLevelProvider)
        {
            _logicLevelProvider = logicLevelProvider;
        }

        public void SetLogicLevelProvider(ILogicLevelProvider logicLevelProvider)
        {
            Contract.Requires(logicLevelProvider != null);

            _logicLevelProvider = logicLevelProvider;
        }

        public ILogicLevelProvider LogicLevelProvider
        {
            get { return _logicLevelProvider; }
        }

        public LogicLevel GetLogicLevel(NodeInfo nodeInfo)
        {
            return _logicLevelProvider.GetLogicLevel(nodeInfo);
        }

        public bool CanFirtsContainSecond(NodeInfo first, NodeInfo second)
        {
            return _logicLevelProvider.CanFirtsContainSecond(first, second);
        }
    }
}