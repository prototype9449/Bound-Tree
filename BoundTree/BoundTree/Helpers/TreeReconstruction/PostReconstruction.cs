using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.Logic;

namespace BoundTree.Helpers.TreeReconstruction
{
    public class PostReconstruction<T> where T : class, IEquatable<T>, new()
    {
        private readonly SingleTree<T> _minorTree;

        public PostReconstruction(SingleTree<T> minorTree)
        {
            _minorTree = minorTree;
        }

        public void Reconstruct(DoubleNode<T> doubleNode)
        {
            var stack = new Stack<DoubleNode<T>>(new[] { doubleNode });
            var passedNodes = new HashSet<DoubleNode<T>>();
            var initialChildIds = doubleNode.Nodes.Select(node => node.MinorLeaf.Id).ToList();

            while (stack.Any())
            {
                var current = stack.Pop();

                if (!current.Nodes.Any()) continue;

                if (passedNodes.Contains(current))
                {
                    current.Nodes.ForEach(node => passedNodes.Add(node));
                    continue;
                }

                var descendants = current.Nodes.Select(node => GetRepairedNode(current, node)).ToList();
                current.Nodes = descendants.Select(pair => pair.Value).ToList();
                for (int i = 0; i < current.Nodes.Count; i++)
                {
                    var currentDoubleNode = descendants[i].Value;

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
            var isDone = false;
            var containHelper = new ContainHelper();

            var singleAscendant = _minorTree.GetParent(child.MinorLeaf.Id);

            if (singleAscendant.Node.Id == parent.MinorLeaf.Id)
            {
                return new KeyValuePair<bool, DoubleNode<T>>(isDone, child);
            }

            var doubleAscendant = child;

            while (containHelper.CreateHelper(parent.MinorLeaf.NodeInfo).CanContain(doubleAscendant.MinorLeaf.NodeInfo))
            {
                doubleAscendant = new DoubleNode<T>(new Node<T>(), singleAscendant.Node)
                {
                    Nodes = new List<DoubleNode<T>>(new[] { doubleAscendant })
                };
                isDone = true;
            }

            return new KeyValuePair<bool, DoubleNode<T>>(isDone, doubleAscendant);
        }


        private void GroupDoubleNodes(DoubleNode<T> doubleNode, List<T> initialChildIds)
        {
            var queue = new Queue<DoubleNode<T>>(new[] { doubleNode });

            while (queue.Any())
            {
                var current = queue.Dequeue();
                if(initialChildIds.Contains(current.MinorLeaf.Id)) continue;

                var groupedNodes = doubleNode.Nodes
                    .Where(node => !node.MainLeaf.IsEmpty())
                    .GroupBy(node => node.MinorLeaf.Id)
                    .Where(group => group.Count() > 1);

                var repairedNodes = groupedNodes.Select(GetRepairedNode).ToList();

                doubleNode.Nodes.RemoveAll(repairedNode 
                    => repairedNodes.Exists(node => node.MinorLeaf.Id == repairedNode.MinorLeaf.Id));

                repairedNodes.ForEach(doubleNode.Nodes.Add);
            }
        }

        private DoubleNode<T> GetRepairedNode(IGrouping<T, DoubleNode<T>> group)
        {
            var doubleNode = new DoubleNode<T>(new Node<T>(), group.First().MinorLeaf)
            {
                Nodes = group.Select(node => node.Nodes.First()).ToList()
            };

            return doubleNode;
        }
    }
}