using System;

namespace BoundTree.Logic.Nodes
{
    [Serializable]
    public class PredefinedList : NodeInfo
    {
        public PredefinedList()
        {
            LogicLevel = 4;
        }
    }
}
