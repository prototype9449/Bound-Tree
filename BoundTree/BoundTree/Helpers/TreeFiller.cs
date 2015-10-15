using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using BoundTree.Nodes;

namespace BoundTree.Helpers
{
    public class TreeFiller<T> where T : class, IEquatable<T>
    {
        public DoubleNode<T> GetFilledTree(Tree<T> mainTree, Tree<T> minorTree, BindingHandler<T> bindingHandler)
        {
            var clonedMainTree = mainTree.Clone();
            var doubleNode = GetDoubleNode(clonedMainTree, mainTree, bindingHandler);

            return doubleNode;
        }
        private Queue<SomeType> CreateAnonymQueue<SomeType>(SomeType value)
        {
            var queue = new Queue<SomeType>();
            queue.Enqueue(value);
            return queue;
        }

        private DoubleNode<T> GetDoubleNode(Tree<T> tree, Tree<T> minorTree, BindingHandler<T> bindingHandler)
        {
            var dictionary = bindingHandler.BoundNodes.ToDictionary(pair => pair.Key, pair => minorTree.GetById(pair.Value));
            var result = new DoubleNode<T>(new Cortege<T>(tree.Root));
            var root = new { node = tree.Root, doubleNode = result };
            var queue = CreateAnonymQueue(root);

            while (queue.Any())
            {
                var current = queue.Dequeue();
                var mainCurrentId = current.doubleNode.MainLeaf.Id;

                if (dictionary.ContainsKey(mainCurrentId))
                {
                    current.doubleNode.MinorLeaf = new Cortege<T>(dictionary[mainCurrentId].Id, dictionary[mainCurrentId].NodeInfo);
                    current.doubleNode.ConnectionKind = ConnectionKind.Strict;
                }
                else
                {
                    current.doubleNode.MinorLeaf = new Cortege<T>(current.doubleNode.MainLeaf.Id, new EmptyNodeInfo());
                    current.doubleNode.ConnectionKind = ConnectionKind.None;
                }

                foreach (var node in current.node.Nodes)
                {
                    var doubleNode = new DoubleNode<T>(node);
                    queue.Enqueue(new { node, doubleNode});
                    current.doubleNode.Add(doubleNode);
                }
            }

            return result;
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

        private Cortege<T> GetMostCommonParent(List<Node<T>> nodes)
        {
            if (nodes.Any(node => node != nodes.First()))
                throw new InvalidOperationException("nodes are not the same");

            var children = new List<Node<T>>();
            nodes.ForEach(node => children.AddRange(node.Nodes));

            Cortege<T> mostCommonParent = null;
            while (children.Distinct().Count() != 1)
            {
                mostCommonParent = new Cortege<T>(children.First().Id, children.First().NodeInfo);
                children.ForEach(node => node.NodeInfo = new EmptyNodeInfo());
                children.Clear();
                children.ForEach(node => children.AddRange(node.Nodes));
            }
            if (mostCommonParent == null)
                throw new InvalidOperationException("most common parent is null");

            return mostCommonParent;
        }

        private Cortege<T> GetCommonParent(IEnumerable<Node<T>> nodes, Tree<T> minorTree)
        {
            var filteredNodes = nodes.Where(node => !(node.NodeInfo is EmptyNodeInfo));
            if (!filteredNodes.Any())
                return new Cortege<T>(null, new EmptyNodeInfo());

            var parentNodes = filteredNodes.Select(node => minorTree.GetParent(node.Id));

            if (parentNodes.All(node => node == parentNodes.First()))
                return new Cortege<T>(parentNodes.First().Id, parentNodes.First().NodeInfo);

            return new Cortege<T>(null, new EmptyNodeInfo());
        }
    }
}