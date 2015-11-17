using BoundTree.Logic.Nodes;

namespace BoundTree.Logic
{
    public class NodeInfoFactory
    {
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
