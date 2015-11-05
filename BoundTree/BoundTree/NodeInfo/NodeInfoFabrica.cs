using BoundTree.NodeInfo.Answers;
using BoundTree.NodeInfo.Questions;

namespace BoundTree.NodeInfo
{
    public class NodeInfoFabrica
    {
        public INodeInfo GridQuestion
        {
            get { return new GridQuestionInfo();}
        }

        public INodeInfo Root
        {
            get { return new RootNodeInfo(); }
        }

        public INodeInfo SingleQustion
        {
            get { return new SingleQuestionInfo(); }
        }

        public INodeInfo EmptyQuestion
        {
            get { return new EmptyNodeInfo(); }
        }

        public INodeInfo OpenTextInfo
        {
            get { return new OpenTextInfo(); }
        }
    }
}