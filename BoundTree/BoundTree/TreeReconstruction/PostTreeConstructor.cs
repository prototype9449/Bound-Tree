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
    public class PostTreeConstructor<T> where T : class, IID<T>, IEquatable<T>, new()
    {
        private readonly SingleTree<T> _minorTree;

        public PostTreeConstructor(SingleTree<T> minorTree)
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

                if (current.Nodes.All(node => node.IsMinorEmpty()))
                    continue;

                if (passedNodes.Contains(current))
                {
                    current.Nodes.ForEach(node => passedNodes.Add(node));
                    continue;
                }

                var initialChildIds = current.Nodes
                    .Select(node => node.MinorLeaf.Id).ToList();

                var descendantPairs = current.Nodes
                    .Select(node => GetRepairedNode(current, node)).ToList();

                if(!descendantPairs.Any()) 
                    continue;

                current.Nodes = descendantPairs.Select(pair => pair.Value).ToList();
                for (int i = 0; i < current.Nodes.Count; i++)
                {
                    var currentDoubleNode = descendantPairs[i].Value;
                    
                    if(!currentDoubleNode.Nodes.Any())
                        passedNodes.Add(currentDoubleNode);

                    while (currentDoubleNode.Nodes.Count() == 1 && currentDoubleNode.MinorLeaf.Id != initialChildIds[i])
                    {
                        passedNodes.Add(currentDoubleNode);
                        currentDoubleNode = currentDoubleNode.Nodes.First();
                    }
                }

                if (descendantPairs.Exists(node => node.Key == true))
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
            Contract.Requires(!parent.MinorLeaf.IsEmpty());
            Contract.Ensures(Contract.Result<KeyValuePair<bool, DoubleNode<T>>>().Value != null);
            

            if(child.IsMinorEmpty()) 
                return new KeyValuePair<bool, DoubleNode<T>>(false, child);

            var isDone = false;

            var singleAscendant = _minorTree.GetParent(child.MinorLeaf.Id);

            //Если он уже родитель, то есть нет промежуточных узлов
            if (singleAscendant.SingleNodeData.Id == parent.MinorLeaf.Id)
            {
                return new KeyValuePair<bool, DoubleNode<T>>(isDone, child);
            }

            var doubleAscendant = child;

            var canAscendantConatin = CanParentContainAscendant(parent, doubleAscendant, singleAscendant);

            while (parent.MinorLeaf.CanContain(singleAscendant.SingleNodeData) && canAscendantConatin)
            {
                doubleAscendant = new DoubleNode<T>(new MultiNodeData<T>(parent.MainLeaf.Width), singleAscendant.SingleNodeData)
                {
                    Nodes = new List<DoubleNode<T>>(new[] { doubleAscendant })
                };
                isDone = true;
                singleAscendant = _minorTree.GetParent(doubleAscendant.MinorLeaf.Id);
                canAscendantConatin = CanParentContainAscendant(parent, doubleAscendant, singleAscendant);
            }

            return new KeyValuePair<bool, DoubleNode<T>>(isDone, doubleAscendant);
        }

        private bool CanParentContainAscendant(DoubleNode<T> parent, DoubleNode<T> child, SingleNode<T> intermediate)
        {
            var minorEmptyNodes = parent.Nodes
                .Where(doubleNode => doubleNode.IsMinorEmpty())
                .Where(doubleNode => doubleNode.MinorLeaf.Id != child.MinorLeaf.Id)
                .ToList();

            var childs = new List<DoubleNode<T>>();
            minorEmptyNodes.ForEach(doubleNode => childs.AddRange(doubleNode.ToList()));
            childs.RemoveAll(doubleNode => doubleNode.IsMinorEmpty());

            var minorOrignalChilds = new List<SingleNode<T>>();
            intermediate.Childs.ForEach(node => minorOrignalChilds.AddRange(node.ToList()));

            var isThereInside = childs.Exists(doubleNode => minorOrignalChilds.Exists(node => doubleNode.MinorLeaf.Id == node.SingleNodeData.Id));

            return !isThereInside;
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

                var groupedNodes = current.Nodes
                    .Where(node => !node.IsMinorEmpty())
                    .GroupBy(node => node.MinorLeaf.Id)
                    .Where(group => group.Count() > 1);

                if (!groupedNodes.Any()) 
                    continue;

                var repairedNodes = groupedNodes.Select(group => GetRepairedNode(group, doubleNode.MainLeaf.Width)).ToList();

                current.Nodes.RemoveAll(repairedNode
                    => repairedNodes.Exists(node => node.MinorLeaf.Id == repairedNode.MinorLeaf.Id));

                repairedNodes.ForEach(current.Nodes.Add);

                queue.Clear();
                current.Nodes.ForEach(queue.Enqueue);
            }
        }

        private DoubleNode<T> GetRepairedNode(IGrouping<T, DoubleNode<T>> group, int width)
        {
            Contract.Requires(group != null);
            Contract.Ensures(Contract.Result<DoubleNode<T>>() != null);

            var doubleNode = new DoubleNode<T>(new MultiNodeData<T>(width), group.First().MinorLeaf)
            {
                Nodes = group.Select(node => node.Nodes.First()).ToList()
            };

            return doubleNode;
        }
    }
}