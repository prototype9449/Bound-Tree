using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using BoundTree.Logic.LogicLevelProviders;
using BoundTree.Logic.Nodes;
using BoundTree.Logic.Nodes.GeneralNodes;
using Single = BoundTree.Logic.Nodes.GeneralNodes.Single;

namespace BoundTree.Logic
{
    [Serializable]
    public class NodeInfoFactory
    {
        private LogicLevelFactory _logicLevelFactory;
        private Dictionary<string, NodeInfo> _nodeTypes;

        public NodeInfoFactory(ILogicLevelProvider logicLevelProvider)
        {
            _logicLevelFactory =  new LogicLevelFactory(logicLevelProvider);
            InitalizeNodeTypes();
        }

        private void InitalizeNodeTypes()
        {
            _nodeTypes = new Dictionary<string, NodeInfo>
            {
                {"Single", new Single(_logicLevelFactory.LogicLevelProvider)},
                {"Grid", new Grid(_logicLevelFactory.LogicLevelProvider)},
                {"Multi", new Multi(_logicLevelFactory.LogicLevelProvider)},
                {"MultiGrid", new MultiGrid(_logicLevelFactory.LogicLevelProvider)},
                {"OpenText", new OpenText(_logicLevelFactory.LogicLevelProvider)},
                {"Grid3D", new Grid3D(_logicLevelFactory.LogicLevelProvider)},
                {"Answer", new Answer(_logicLevelFactory.LogicLevelProvider)},
                {"PredefinedList", new PredefinedList(_logicLevelFactory.LogicLevelProvider)}
            };
        }
        
        public ILogicLevelProvider LogicLevelProvider
        {
            get { return _logicLevelFactory.LogicLevelProvider; }
        }

        public void SetLogicLevelProvider(ILogicLevelProvider logicLevelProvider)
        {
            Contract.Requires(logicLevelProvider != null);

            _logicLevelFactory.SetLogicLevelProvider(LogicLevelProvider);
        }

        public NodeInfo GetNodeInfo(string nodeInfoType)
        {
            Contract.Requires(!string.IsNullOrEmpty(nodeInfoType));
            Contract.Exists(_nodeTypes, node => node.Key == nodeInfoType);

            return _nodeTypes[nodeInfoType];
        }

        public bool Contains(string nodeInfoType)
        {
            Contract.Requires(!string.IsNullOrEmpty(nodeInfoType));

            return _nodeTypes.ContainsKey(nodeInfoType);
        }

        public NodeInfo Grid
        {
            get { return new Grid(_logicLevelFactory.LogicLevelProvider); }
        }

        public NodeInfo Root
        {
            get { return new Root(_logicLevelFactory.LogicLevelProvider); }
        }

        public NodeInfo Single
        {
            get { return new Single(_logicLevelFactory.LogicLevelProvider); }
        }

        public NodeInfo Empty
        {
            get { return new Empty(_logicLevelFactory.LogicLevelProvider); }
        }

        public NodeInfo OpenText
        {
            get { return new OpenText(_logicLevelFactory.LogicLevelProvider); }
        }

        public NodeInfo Grid3D
        {
            get { return new Grid3D(_logicLevelFactory.LogicLevelProvider); }
        }

        public NodeInfo Multi
        {
            get { return new Multi(_logicLevelFactory.LogicLevelProvider); }
        }

        public NodeInfo MultiGrid
        {
            get { return new MultiGrid(_logicLevelFactory.LogicLevelProvider); }
        }

        public NodeInfo PredefinedList
        {
            get { return new PredefinedList(_logicLevelFactory.LogicLevelProvider); }
        }

        public Answer Answer
        {
            get { return new Answer(_logicLevelFactory.LogicLevelProvider); }
        }
    }
}
