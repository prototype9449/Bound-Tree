using System.Collections.Generic;
using BoundTree.NodeInfo;

namespace BoundTree
{
    public class SingleNodeFabrica
    {
        public SingleNode<StringId> GetNode(string id, INodeInfo nodeInfo)
        {
            return new SingleNode<StringId>(new StringId(id), nodeInfo);
        }

        public SingleNode<StringId> GetNode(string id, INodeInfo nodeInfo, IList<SingleNode<StringId>> nodes)
        {
            return new SingleNode<StringId>(new StringId(id), nodeInfo, nodes);
        }
    }
}