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
        private readonly LogicLevelFactory _logicLevelFactory;
        private Dictionary<string, NodeInfo> _nodeTypes;

        public NodeInfoFactory(LogicLevelFactory logicLevelProvider)
        {
            _logicLevelFactory = logicLevelProvider;
            InitalizeNodeTypes();
        }

        private void InitalizeNodeTypes()
        {
            _nodeTypes = new Dictionary<string, NodeInfo>
            {
                {"Single", new Single(_logicLevelFactory)},
                {"Grid", new Grid(_logicLevelFactory)},
                {"Multi", new Multi(_logicLevelFactory)},
                {"MultiGrid", new MultiGrid(_logicLevelFactory)},
                {"OpenText", new OpenText(_logicLevelFactory)},
                {"Grid3D", new Grid3D(_logicLevelFactory)},
                {"Answer", new Answer(_logicLevelFactory)},
                {"PredefinedList", new PredefinedList(_logicLevelFactory)}
            };
        }

        public void SetLogicLevelProvider(ILogicLevelProvider logicLevelProvider)
        {
            Contract.Requires(logicLevelProvider != null);

            _logicLevelFactory.SetLogicLevelProvider(logicLevelProvider);
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
            get { return new Grid(_logicLevelFactory); }
        }

        public NodeInfo Root
        {
            get { return new Root(_logicLevelFactory); }
        }

        public NodeInfo Single
        {
            get { return new Single(_logicLevelFactory); }
        }

        public NodeInfo Empty
        {
            get { return new Empty(_logicLevelFactory); }
        }

        public NodeInfo OpenText
        {
            get { return new OpenText(_logicLevelFactory); }
        }

        public NodeInfo Grid3D
        {
            get { return new Grid3D(_logicLevelFactory); }
        }

        public NodeInfo Multi
        {
            get { return new Multi(_logicLevelFactory); }
        }

        public NodeInfo MultiGrid
        {
            get { return new MultiGrid(_logicLevelFactory); }
        }

        public NodeInfo PredefinedList
        {
            get { return new PredefinedList(_logicLevelFactory); }
        }

        public Answer Answer
        {
            get { return new Answer(_logicLevelFactory); }
        }
    }
}
