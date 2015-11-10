namespace BoundTree.Logic.Nodes
{
    public class Empty : NodeInfo
    {
        public Empty()
        {
            LogicLevel = -1;
        }

        public override bool IsEmpty()
        {
            return true;
        }
    }
}