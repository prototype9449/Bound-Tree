using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.NodeInfo;

namespace BoundTree.Helpers
{
    public class TreeFiller<T> where T : class, IEquatable<T>, new()
    {
        private readonly BindContoller<T> _bindContoller;

        public TreeFiller(BindContoller<T> bindContoller)
        {
            _bindContoller = bindContoller;
        }

        public DoubleNode<T> GetFilledTree()
        {
            var clonedMainTree = _bindContoller.MainSingleTree.Clone();
            var connections = _bindContoller.BindingHandler.BoundNodes
                .ToDictionary(pair => pair.Key, pair => _bindContoller.MinorSingleTree.GetById(pair.Value));

            var doubleNode = GetDoubleNode(clonedMainTree, connections);
            return doubleNode;
        }

        private DoubleNode<T> GetDoubleNode(SingleTree<T> mainTree, Dictionary<T, SingleNode<T>> connections)
        {
            var resultDoubleNode = new DoubleNode<T>(mainTree.Root);

            var root = new { node = mainTree.Root, doubleNode = resultDoubleNode };
            var queue   = new Queue<dynamic>(new[] {root});

            while (queue.Any())
            {
                var current = queue.Dequeue();
                var mainCurrentId = current.doubleNode.MainLeaf.Id;

                if (connections.ContainsKey(mainCurrentId))
                {
                    current.doubleNode.MinorLeaf = connections[mainCurrentId];
                    current.doubleNode.ConnectionKind = ConnectionKind.Strict;
                }
                else
                {
                    current.doubleNode.MinorLeaf = new Node<T>(current.doubleNode.MainLeaf.Id, -1, new EmptyNodeInfo());
                    current.doubleNode.ConnectionKind = ConnectionKind.None;
                }

                foreach (var node in current.node.Nodes)
                {
                    var doubleNode = new DoubleNode<T>(node);
                    queue.Enqueue(new {node, doubleNode});
                    current.doubleNode.Add(doubleNode);
                }
            }

            return resultDoubleNode;
        }

//        private void RestoreRestNodes(DoubleNode<T> doubleNode, Tree<T> minorTree)
//        {
//            var stack = new Stack<DoubleNode<T>>();
//            stack.Push(doubleNode);
//            var markedNodes = new HashSet<DoubleNode<T>>();
//
//            while (stack.Any())
//            {
//                var currentNode = stack.Peek();
//                if (!currentNode.Nodes.Any())
//                {
//                    markedNodes.Add(currentNode);
//                    stack.Pop();
//                }
//                else if (currentNode.Nodes.All(markedNodes.Contains))
//                {
//                    var areTheSame = currentNode.Nodes.Any(node => node == currentNode.Nodes.First());
//                    var commonParent = areTheSame
//                        ? GetMostCommonParent(currentNode.Nodes)
//                        : GetCommonParent(currentNode.Nodes, minorTree);
//
//                    if (currentNode.Id.Equals(commonParent.Id))
//                    {
//                        currentNode.NodeInfo = commonParent.NodeInfo;
//                    }
//                    else
//                    {
//                        //todo we should add the node in both trees
//                    }
//
//                    markedNodes.Add(stack.Pop());
//                }
//                else
//                    currentNode.Nodes.ForEach(stack.Push);
//
//            }
//        }

//        private Cortege<T> GetMostCommonParent(List<SingleNode<T>> nodes)
//        {
//            if (nodes.Any(node => node != nodes.First()))
//                throw new InvalidOperationException("nodes are not the same");
//
//            var children = new List<SingleNode<T>>();
//            nodes.ForEach(node => children.AddRange(node.Nodes));
//
//            Cortege<T> mostCommonParent = null;
//            while (children.Distinct().Count() != 1)
//            {
//                mostCommonParent = new Cortege<T>(children.First().Id, children.First().NodeInfo);
//                children.ForEach(node => node.NodeInfo = new EmptyNodeInfo());
//                children.Clear();
//                children.ForEach(node => children.AddRange(node.Nodes));
//            }
//            if (mostCommonParent == null)
//                throw new InvalidOperationException("most common parent is null");
//
//            return mostCommonParent;
//        }
//
//        private Cortege<T> GetCommonParent(IEnumerable<SingleNode<T>> nodes, SingleTree<T> minorSingleTree)
//        {
//            var filteredNodes = nodes.Where(node => !(node.NodeInfo is EmptyNodeInfo));
//            if (!filteredNodes.Any())
//                return new Cortege<T>(null, new EmptyNodeInfo());
//
//            var parentNodes = filteredNodes.Select(node => minorSingleTree.GetParent(node.Id));
//
//            if (parentNodes.All(node => node == parentNodes.First()))
//                return new Cortege<T>(parentNodes.First().Id, parentNodes.First().NodeInfo);
//
//            return new Cortege<T>(null, new EmptyNodeInfo());
//        }
    }
}