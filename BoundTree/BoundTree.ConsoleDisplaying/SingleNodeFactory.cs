using System.Collections.Generic;
using System.Diagnostics.Contracts;
using BoundTree.Logic;
using BoundTree.Logic.Nodes;
using BoundTree.Logic.TreeNodes;

namespace BoundTree.ConsoleDisplaying
{
    public class SingleNodeFactory
    {
        public SingleNode<StringId> GetNode(string id, NodeInfo nodeInfo)
        {
            Contract.Requires(!string.IsNullOrEmpty(id));
            Contract.Requires(nodeInfo != null);
            Contract.Ensures(Contract.Result<SingleNode<StringId>>() != null);

            return new SingleNode<StringId>(new StringId(id), nodeInfo);
        }

        public SingleNode<StringId> GetNode(string id, NodeInfo nodeInfo, IList<SingleNode<StringId>> nodes)
        {
            Contract.Requires(!string.IsNullOrEmpty(id));
            Contract.Requires(nodeInfo != null);
            Contract.Requires(nodes != null);
            Contract.Ensures(Contract.Result<SingleNode<StringId>>() != null);

            return new SingleNode<StringId>(new StringId(id), nodeInfo, nodes);
        }
    }
}