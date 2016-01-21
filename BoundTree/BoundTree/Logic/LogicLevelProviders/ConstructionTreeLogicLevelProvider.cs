using System;
using System.Collections.Generic;
using BoundTree.Logic.Nodes;
using BoundTree.Logic.Nodes.GeneralNodes;
using Single = BoundTree.Logic.Nodes.GeneralNodes.Single;

namespace BoundTree.Logic.LogicLevelProviders
{
    [Serializable]
    public class ConstructionTreeLogicLevelProvider : ILogicLevelProvider
    {
        private readonly Dictionary<Type, LogicLevel> _logicLevels;

        public ConstructionTreeLogicLevelProvider()
        {
            _logicLevels = new Dictionary<Type, LogicLevel>
            {
                {typeof(Root), new LogicLevel(0)},
                {typeof(Grid3D), new LogicLevel(1)},
                {typeof(MultiGrid), new LogicLevel(2)},
                {typeof(Grid), new LogicLevel(3)},
                {typeof(Multi), new LogicLevel(4)},
                {typeof(Single), new LogicLevel(5)},
                {typeof(OpenText), new LogicLevel(5)},
                {typeof(PredefinedList), new LogicLevel(6)},
                {typeof(Answer), new LogicLevel(7)}
            };
        }

        public LogicLevel GetLogicLevel(NodeInfo nodeInfo)
        {
            if (!_logicLevels.ContainsKey(nodeInfo.GetType()))
                throw new TypeAccessException();

            return _logicLevels[nodeInfo.GetType()];
        }

        public bool CanFirtsContainSecond(NodeInfo first, NodeInfo second)
        {
            return GetLogicLevel(first) < GetLogicLevel(second);
        }
    }
}