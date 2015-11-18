using System.Collections.Generic;
using BoundTree.Logic.Nodes;
using BoundTree.Logic.Nodes.GeneralNodes;

namespace BoundTree.Logic
{
    public class NodeInfoFactory
    {
        private static Dictionary<string, NodeInfo> _nodeTypes = new Dictionary<string, NodeInfo>();

        static NodeInfoFactory()
        {
            _nodeTypes.Add("Single", new Single());
            _nodeTypes.Add("Grid", new Grid());
            _nodeTypes.Add("Multi", new Multi());
            _nodeTypes.Add("MultiGrid", new MultiGrid());
            _nodeTypes.Add("OpenText", new OpenTextInfo());
            _nodeTypes.Add("Grid3D", new Grid3D());
            _nodeTypes.Add("Answer", new Answer());
            _nodeTypes.Add("PredefinedList", new PredefinedList());
        }

        public static NodeInfo GetNodeInfo(string nodeInfoType)
        {
            return _nodeTypes[nodeInfoType];
        }

        public static bool Contains(string nodeInfoType)
        {
            return _nodeTypes.ContainsKey(nodeInfoType);
        }

        public NodeInfo Grid
        {
            get { return new Grid(); }
        }

        public NodeInfo Root
        {
            get { return new Root(); }
        }

        public NodeInfo Single
        {
            get { return new Single(); }
        }

        public NodeInfo Empty
        {
            get { return new Empty(); }
        }

        public NodeInfo OpenText
        {
            get { return new OpenTextInfo(); }
        }

        public NodeInfo Grid3D
        {
            get { return new Grid3D(); }
        }

        public NodeInfo Multi
        {
            get { return new Multi(); }
        }

        public NodeInfo MultiGrid
        {
            get { return new MultiGrid(); }
        }

        public NodeInfo PredefinedList
        {
            get { return new PredefinedList(); }
        }

        public Answer Answer
        {
            get { return new Answer(); }
        }
    }
}
