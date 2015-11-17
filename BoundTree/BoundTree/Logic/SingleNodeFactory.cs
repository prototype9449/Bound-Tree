using System.Collections.Generic;
using BoundTree.Logic.Nodes;

namespace BoundTree.Logic
{
    public class SingleNodeFactory
    {
        public SingleNode<StringId> GetNode(string id, NodeInfo nodeInfo)
        {
            return new SingleNode<StringId>(new StringId(id), nodeInfo);
        }

        public SingleNode<StringId> GetNode(string id, NodeInfo nodeInfo, IList<SingleNode<StringId>> nodes)
        {
            return new SingleNode<StringId>(new StringId(id), nodeInfo, nodes);
        }
    }
}