using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.Nodes;

namespace BoundTree.Helpers
{
    public class TreeFiller
    {
        public Tree GetFilledTree(Tree mainTree, Tree minorTree, BindingHandler bindingHandler)
        {
            var tree = mainTree.Clone();
            var dictionary = bindingHandler.BoundNodes.ToDictionary(pair => pair.Key, pair => minorTree.GetById(pair.Value));

            ReplaceLeafs(tree, dictionary);

            return tree;
        }

        private void ReplaceLeafs(Tree tree, Dictionary<int, Node> dictionary)
        {
            var queue = new Queue<Node>();
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
        }

        private void RestoreRestNodes(Tree tree, Tree minorTree, Dictionary<int, Node> dictionary)
        {
            var stack = new Stack<Node>();
            stack.Push(tree.Root);
            var markedNodes = new HashSet<Node>();

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
                    currentNode.NodeInfo = TryGetCommonParent(currentNode.Nodes);
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
    }
}