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

            RestoreRestNodes(tree, minorTree);
        }

        private void RestoreRestNodes(Tree<T> tree, Tree<T> minorTree)
        {
            var stack = new Stack<Node<T>>();
            stack.Push(tree.Root);
            var markedNodes = new HashSet<Node<T>>();

            while (stack.Any())
            {
                var currentNode = stack.Peek();
                if (!currentNode.Nodes.Any())
                {
                    markedNodes.Add(currentNode);
                    stack.Pop();
                }
                else if (currentNode.Nodes.All(markedNodes.Contains))
                {
                    var areTheSame = currentNode.Nodes.Any(node => node == currentNode.Nodes.First());
                    var commonParent = areTheSame
                        ? GetMostCommonParent(currentNode.Nodes)
                        : GetCommonParent(currentNode.Nodes, minorTree);

                    if (currentNode.Id.Equals(commonParent.Id))
                    {
                        currentNode.NodeInfo = commonParent.NodeInfo;
                    }
                    else
                    {
                        //todo we should add the node in both trees
                    }

                    markedNodes.Add(stack.Pop());
                }
                else
                    currentNode.Nodes.ForEach(stack.Push);

            }
        }

        private Cortege GetMostCommonParent(List<Node<T>> nodes)
        {
            if (nodes.Any(node => node != nodes.First()))
                throw new InvalidOperationException("nodes are not the same");

            var children = new List<Node<T>>();
            nodes.ForEach(node => children.AddRange(node.Nodes));

            Cortege? mostCommonParent = null;
            while (children.Distinct().Count() != 1)
            {
                mostCommonParent = new Cortege( children.First().Id,children.First().NodeInfo);
                children.ForEach(node => node.NodeInfo = new EmptyNodeInfo());
                children.Clear();
                children.ForEach(node => children.AddRange(node.Nodes));
            }
            if (!mostCommonParent.HasValue)
                throw new InvalidOperationException("most common parent is null");

            return mostCommonParent.Value;
        }

        private Cortege GetCommonParent(IEnumerable<Node<T>> nodes, Tree<T> minorTree)
        {
            var filteredNodes = nodes.Where(node => !(node.NodeInfo is EmptyNodeInfo));
            if (!filteredNodes.Any())
                return new Cortege(null, new EmptyNodeInfo());

            var parentNodes = filteredNodes.Select(node => minorTree.GetParent(node.Id));

            if (parentNodes.All(node => node == parentNodes.First()))
                return new Cortege(parentNodes.First().Id, parentNodes.First().NodeInfo);

            return new Cortege(null, new EmptyNodeInfo());

        }
        private struct Cortege
        {
            public INodeInfo NodeInfo { get; private set; }
            public T Id { get; private set; }

            public Cortege(T id, INodeInfo nodeInfo): this()
            {
                Id = id;
                NodeInfo = nodeInfo;
            }
        }
    }

    
}