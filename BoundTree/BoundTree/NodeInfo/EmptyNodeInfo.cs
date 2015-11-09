namespace BoundTree.NodeInfo
{
    public class EmptyNodeInfo : NodeInfo
    {
        public EmptyNodeInfo()
        {
            LogicLevel = -1;
        }

        public override bool IsEmpty()
        {
            return true;
        }
    }
}