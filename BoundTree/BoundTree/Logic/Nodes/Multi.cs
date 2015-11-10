using System;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public class Multi : NodeInfo
    {
        public Multi()
        {
            LogicLevel = 2;
        }
    }
}