using System;
using System.Diagnostics.Contracts;

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
    }
}