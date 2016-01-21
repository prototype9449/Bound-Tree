using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Interfaces;
using BoundTree.Logic;
using BoundTree.Logic.NodeData;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;

namespace BoundTree.TreeReconstruction
{
    public class ConnectionContructor<T> where T : class, IId<T>, IEquatable<T>, new()
    {
        private readonly NodeInfoFactory _nodeInfoFactory;

        public ConnectionContructor(NodeInfoFactory nodeInfoFactory)
        {
            _nodeInfoFactory = nodeInfoFactory;
        }

        public DoubleNode<T> GetDoubleNodeWithConnections(MultiTree<T> mainTree, Dictionary<T, SingleNode<T>> connections)
        {
            Contract.Requires(mainTree != null);
            Contract.Requires(connections != null);
            Contract.Requires(connections.Any());
            Contract.Ensures(Contract.Result<DoubleNode<T>>() != null);

            var resultDoubleNode = new DoubleNode<T>(mainTree.Root, _nodeInfoFactory);

            var root = new { multiNode = mainTree.Root, doubleNode = resultDoubleNode };
            var queue = GetQueue(root);

            while (queue.Any())
            {
                var current = queue.Dequeue();
                var mainCurrentId = current.doubleNode.MainLeaf.Id;

                if (connections.ContainsKey(mainCurrentId))
                {
                    current.doubleNode.MinorLeaf = connections[mainCurrentId].SingleNodeData;
                    current.doubleNode.ConnectionKind = ConnectionKind.Strict;
                }
                else
                {
                    var depth = current.doubleNode.MainLeaf.Depth;
                    current.doubleNode.MinorLeaf = new SingleNodeData<T>(new NodeData<T>(new T(), depth, _nodeInfoFactory.Empty));
                    current.doubleNode.ConnectionKind = ConnectionKind.None;
                }

                foreach (var multiNode in current.multiNode.Childs)
                {
                    var doubleNode = new DoubleNode<T>(multiNode, _nodeInfoFactory);
                    queue.Enqueue(new { multiNode, doubleNode });
                    current.doubleNode.Add(doubleNode);
                }
            }

            return resultDoubleNode;
        }

        private Queue<Ttype> GetQueue<Ttype>(Ttype item)
        {
            return new Queue<Ttype>(new[] { item });
        } 
    }
}