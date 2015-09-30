using BoundTree.Nodes.Questions;

namespace BoundTree.Nodes
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
    }
}