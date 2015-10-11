using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.Nodes;

namespace BoundTree.Helpers
{
    public class TreeFiller<T> where T : class, IEquatable<T>
    {
        public Tree<T> GetFilledTree(Tree<T> mainTree, Tree<T> minorTree, BindingHandler<T> bindingHandler)
        {
            var tree = mainTree.Clone();
            var dictionary = bindingHandler.BoundNodes.ToDictionary(pair => pair.Key, pair => minorTree.GetById(pair.Value));

            ReplaceLeafs(tree, dictionary, minorTree);
            
            return tree;
        }

        private void ReplaceLeafs(Tree<T> tree, Dictionary<T, Node<T>> dictionary, Tree<T> minorTree)
        {
            var queue = new Queue<Node<T>>();
            queue.Enqueue(tree.Root);
            while (queue.Count != 0)
            {
                var currentId = queue.Peek().Id;
                var currentNode = tree.GetById(currentId);
                if (dictionary.ContainsKey(currentId))
                {
                    currentNode.NodeInfo = dictionary[currentId].NodeInfo;
                }
                else
                {
                    currentNode.NodeInfo = new EmptyNodeInfo();
                }

                foreach (var node in queue.Dequeue().Nodes)
                {
                    queue.Enqueue(node);
                }
            }

            RestoreRestNodes(tree, dictionary, minorTree);
        }

        private void RestoreRestNodes(Tree<T> tree, Dictionary<T, Node<T>> dictionary, Tree<T> minorTree)
        {
            var stack = new Stack<Node<T>>();
            stack.Push(tree.Root);
            var markedNodes = new HashSet<Node<T>>();

            while (stack.Count != 0)
            {
                var currentNode = stack.Peek();
                if (currentNode.Nodes.Count == 0)
                {
                    markedNodes.Add(currentNode);
                    stack.Pop();
                }
                else if(currentNode.Nodes.All(markedNodes.Contains))
                {
                    currentNode.NodeInfo = GetCommonParent(currentNode.Nodes, dictionary, minorTree);
                    markedNodes.Add(stack.Pop());
                }
                else
                {
                    foreach (var node in currentNode.Nodes)
                    {
                        stack.Push(node);
                    }
                }
            }
        }

        private INodeInfo GetCommonParent(List<Node<T>> nodes, Dictionary<T, Node<T>> dictionary, Tree<T> minorTree)
        {
            var filteredNodes = nodes.Where(node => !(node.NodeInfo is EmptyNodeInfo));

            if(!filteredNodes.Any())
                return new EmptyNodeInfo();

            var parentNodes = filteredNodes.Select(node => minorTree.GetParent(node.Id));

            if (parentNodes.All(node => node == parentNodes.First()))
                return parentNodes.First().NodeInfo;

            return new EmptyNodeInfo();

        }
    }
}