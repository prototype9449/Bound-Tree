﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Logic;
using BoundTree.Logic.NodeData;
using BoundTree.Logic.Nodes;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;

namespace BoundTree.TreeReconstruction
{
    public class ConnectionReconstruction<T> where T : class, IEquatable<T>, new()
    {
        public DoubleNode<T> GetDoubleNodeWithConnections(SingleTree<T> mainTree, Dictionary<T, SingleNode<T>> connections)
        {
            Contract.Requires(mainTree != null);
            Contract.Requires(connections != null);
            Contract.Requires(connections.Any());
            Contract.Ensures(Contract.Result<DoubleNode<T>>() != null);

            var resultDoubleNode = new DoubleNode<T>(mainTree.Root);

            var root = new { node = mainTree.Root, doubleNode = resultDoubleNode };
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
                    current.doubleNode.MinorLeaf = new NodeData<T>(new T(), depth, new Empty());
                    current.doubleNode.ConnectionKind = ConnectionKind.None;
                }

                foreach (var node in current.node.Childs)
                {
                    var doubleNode = new DoubleNode<T>(node);
                    queue.Enqueue(new { node, doubleNode });
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