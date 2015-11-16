using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.Logic;
using BoundTree.Logic.ConnectionKinds;
using BoundTree.Logic.Nodes;

namespace BoundTree.Helpers.TreeReconstruction
{
    public class ConnectionReconstruction<T> where T : class, IEquatable<T>, new()
    {
        public DoubleNode<T> GetDoubleNodeWithConnections(SingleTree<T> mainTree, Dictionary<T, SingleNode<T>> connections)
        {
            var resultDoubleNode = new DoubleNode<T>(mainTree.Root);

            var root = new { node = mainTree.Root, doubleNode = resultDoubleNode };
            var queue = GetQueue(root);

            while (queue.Any())
            {
                var current = queue.Dequeue();
                var mainCurrentId = current.doubleNode.MainLeaf.Id;

                if (connections.ContainsKey(mainCurrentId))
                {
                    current.doubleNode.MinorLeaf = connections[mainCurrentId].Node;
                    current.doubleNode.Connection = new StrictConnection();
                }
                else
                {
                    current.doubleNode.MinorLeaf = new Node<T>(new T(), -1, new Empty());
                    current.doubleNode.Connection = new NoneConnection();
                }

                foreach (var node in current.node.Nodes)
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