using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Logic;

namespace BoundTree.TreeReconstruction
{
    public class PostReconstruction<T> where T : class, IEquatable<T>, new()
    {
        private readonly SingleTree<T> _minorTree;

        public PostReconstruction(SingleTree<T> minorTree)
        {
            Contract.Requires(minorTree != null);

            _minorTree = minorTree;
        }

        public void Reconstruct(DoubleNode<T> doubleNode)
        {
            Contract.Requires(doubleNode != null);

            var stack = new Stack<DoubleNode<T>>(new[] { doubleNode });
            var passedNodes = new List<DoubleNode<T>>();

            while (stack.Any())
            {
                var current = stack.Pop();
                current.Nodes.ForEach(stack.Push);

                if(current.IsMinorEmpty() || passedNodes.Exists(node => node.MinorLeaf == current.MinorLeaf)) 
                    continue;

                var initialChildIds = current.Nodes
                    .Select(node => node.MinorLeaf.Id).ToList();

                if (current.Nodes.All(node => node.IsMinorEmpty())) 
                    continue;

                if (passedNodes.Contains(current))
                {
                    current.Nodes.ForEach(node => passedNodes.Add(node));
                    continue;
                }

                var descendants = current.Nodes
                    .Select(node => GetRepairedNode(current, node)).ToList();

                if(!descendants.Any()) 
                    continue;

                current.Nodes = descendants.Select(pair => pair.Value).ToList();
                for (int i = 0; i < current.Nodes.Count; i++)
                {
                    var currentDoubleNode = descendants[i].Value;
                    
                    if(!currentDoubleNode.Nodes.Any())
                        passedNodes.Add(currentDoubleNode);

                    while (currentDoubleNode.Nodes.Count() == 1 && currentDoubleNode.MinorLeaf.Id != initialChildIds[i])
                    {
                        passedNodes.Add(currentDoubleNode);
                        currentDoubleNode = currentDoubleNode.Nodes.First();
                    }
                }

                if (descendants.Exists(node => node.Key == true))
                {
                    GroupDoubleNodes(current, initialChildIds);
                    stack.Clear();
                    stack.Push(doubleNode);
                }
            }
        }

        private KeyValuePair<bool, DoubleNode<T>> GetRepairedNode(DoubleNode<T> parent, DoubleNode<T> child)
        {
            Contract.Requires(parent != null);
            Contract.Requires(child != null);
            Contract.Ensures(Contract.Result<KeyValuePair<bool, DoubleNode<T>>>().Value != null);
            
            if(child.IsMinorEmpty()) 
                return new KeyValuePair<bool, DoubleNode<T>>(false, child);

            var isDone = false;

            var singleAscendant = _minorTree.GetParent(child.MinorLeaf.Id);

            if (singleAscendant.Node.Id == parent.MinorLeaf.Id)
            {
                return new KeyValuePair<bool, DoubleNode<T>>(isDone, child);
            }

            var doubleAscendant = child;

            while (parent.MinorLeaf.CanContain(singleAscendant.Node))
            {
                doubleAscendant = new DoubleNode<T>(new Node<T>(), singleAscendant.Node)
                {
                    Nodes = new List<DoubleNode<T>>(new[] { doubleAscendant })
                };
                isDone = true;
                singleAscendant = _minorTree.GetParent(doubleAscendant.MinorLeaf.Id);
            }

            return new KeyValuePair<bool, DoubleNode<T>>(isDone, doubleAscendant);
        }


        private void GroupDoubleNodes(DoubleNode<T> doubleNode, List<T> initialChildIds)
        {
            Contract.Requires(doubleNode != null);
            Contract.Requires(initialChildIds != null);


            var queue = new Queue<DoubleNode<T>>(new[] { doubleNode });

            while (queue.Any())
            {
                var current = queue.Dequeue();
                if (initialChildIds.Contains(current.MinorLeaf.Id)) 
                    continue;

                var groupedNodes = doubleNode.Nodes
                    .Where(node => !node.IsMinorEmpty())
                    .GroupBy(node => node.MinorLeaf.Id)
                    .Where(group => group.Count() > 1);

                if (!groupedNodes.Any()) 
                    continue;

                var repairedNodes = groupedNodes.Select(GetRepairedNode).ToList();

                doubleNode.Nodes.RemoveAll(repairedNode
                    => repairedNodes.Exists(node => node.MinorLeaf.Id == repairedNode.MinorLeaf.Id));

                repairedNodes.ForEach(doubleNode.Nodes.Add);

                queue.Clear();
                queue.Enqueue(doubleNode);
            }
        }

        private DoubleNode<T> GetRepairedNode(IGrouping<T, DoubleNode<T>> group)
        {
            Contract.Requires(group != null);
            Contract.Ensures(Contract.Result<DoubleNode<T>>() != null);

            var doubleNode = new DoubleNode<T>(new Node<T>(), group.First().MinorLeaf)
            {
                Nodes = group.Select(node => node.Nodes.First()).ToList()
            };

            return doubleNode;
        }
    }
}