using System;

namespace BoundTree.NodeInfo
{
    [Serializable]
    public class RootNodeInfo : INodeInfo
    {
        public RootNodeInfo()
        {
            LogicLevel = 0;
            Type = "Root";
        }

        public int LogicLevel { get; private set; }
        public string Type { get; private set; }
    }
}
