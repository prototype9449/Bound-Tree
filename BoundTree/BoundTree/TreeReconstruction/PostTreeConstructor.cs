using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Interfaces;
using BoundTree.Logic;
using BoundTree.Logic.NodeData;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;
using Build.TestFramework;

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
                current.Childs.ForEach(stack.Push);

                if(current.IsMinorEmpty() || passedNodes.Exists(node => node.MinorLeaf == current.MinorLeaf)) 
                    continue;

                if (current.Childs.All(node => node.IsMinorEmpty()))
                    continue;

                if (passedNodes.Contains(current))
                {
                    current.Childs.ForEach(passedNodes.Add);
                    continue;
                }

                var descendantPairs = current.Childs.Select(node => GetRepairedNode(current, node)).ToList();

                if (!descendantPairs.Any())
                    continue;

                var initialChildIds = current.Childs.Select(node => node.MinorLeaf.Id).ToList();

                current.Childs = descendantPairs.Select(pair => pair.Second).ToList();
                for (int i = 0; i < current.Childs.Count; i++)
                {
                    var currentDoubleNode = descendantPairs[i].Second;
                    
                    if(!currentDoubleNode.Childs.Any())
                        passedNodes.Add(currentDoubleNode);

                    while (currentDoubleNode.Childs.Count() == 1 && currentDoubleNode.MinorLeaf.Id != initialChildIds[i])
                    {
                        passedNodes.Add(currentDoubleNode);
                        currentDoubleNode = currentDoubleNode.Childs.First();
                    }
                }

                if (descendantPairs.Exists(node => node.First == true))
                {
                    GroupDoubleNodes(current, initialChildIds);
                    stack.Clear();
                    stack.Push(doubleNode);
                }
            }
        }

        private Cortege<bool, DoubleNode<T>> GetRepairedNode(DoubleNode<T> majorParent, DoubleNode<T> majorChild)
        {
            Contract.Requires(majorParent != null);
            Contract.Requires(majorChild != null);
            Contract.Requires(!majorParent.MinorLeaf.IsEmpty());
            Contract.Ensures(Contract.Result<Cortege<bool, DoubleNode<T>>>().Second != null);
            
            if(majorChild.IsMinorEmpty()) 
                return new Cortege<bool, DoubleNode<T>>(false, majorChild);

            var isDone = false;

            var intermediateParent = _minorTree.GetParent(majorChild.MinorLeaf.Id);

            //Если он уже родитель, то есть нет промежуточных узлов
            if (intermediateParent.SingleNodeData.Id == majorParent.MinorLeaf.Id)
            {
                return new Cortege<bool, DoubleNode<T>>(isDone, majorChild);
            }

            var result = majorChild;

            var canMajorParentContain = CanParentContainAscendant(majorParent, intermediateParent);

            while (majorParent.MinorLeaf.CanContain(intermediateParent.SingleNodeData) && canMajorParentContain)
            {
                result = new DoubleNode<T>(new MultiNodeData<T>(majorParent.MainLeaf.Width), intermediateParent.SingleNodeData)
                {
                    Childs = new List<DoubleNode<T>>(new[] { result })
                };
                isDone = true;
                intermediateParent = _minorTree.GetParent(result.MinorLeaf.Id);
                canMajorParentContain = CanParentContainAscendant(majorParent, intermediateParent);
            }

            return new Cortege<bool, DoubleNode<T>>(isDone, result);
        }

        private bool CanParentContainAscendant(DoubleNode<T> parent, SingleNode<T> intermediate)
        {
            var minorEmptyNodes = parent.Childs
                .Where(doubleNode => doubleNode.IsMinorEmpty())
                .ToList();

            var childsOfEmptyNodes = new List<DoubleNode<T>>();
            minorEmptyNodes.ForEach(emptyDoubleNode => childsOfEmptyNodes.AddRange(emptyDoubleNode.ToList()));
            childsOfEmptyNodes.RemoveAll(doubleNode => doubleNode.IsMinorEmpty());

            var minorOrignalChilds = new List<SingleNode<T>>();
            intermediate.Childs.ForEach(node => minorOrignalChilds.AddRange(node.ToList()));

            var isThereInside = childsOfEmptyNodes.Any(doubleNode => minorOrignalChilds.Any(node => doubleNode.MinorLeaf.Id == node.SingleNodeData.Id));

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

                var groupedNodes = current.Childs
                    .Where(node => !node.IsMinorEmpty())
                    .GroupBy(node => node.MinorLeaf.Id)
                    .Where(group => group.Count() > 1);

                if (!groupedNodes.Any()) 
                    continue;

                var repairedNodes = groupedNodes.Select(group => GetRepairedNode(group, doubleNode.MainLeaf.Width)).ToList();

                current.Childs.RemoveAll(node
                    => repairedNodes.Exists(repairedNode => repairedNode.MinorLeaf.Id == node.MinorLeaf.Id));

                repairedNodes.ForEach(current.Childs.Add);

                queue.Clear();
                current.Childs.ForEach(queue.Enqueue);
            }
        }

        private DoubleNode<T> GetRepairedNode(IGrouping<T, DoubleNode<T>> group, int width)
        {
            Contract.Requires(group != null);
            Contract.Ensures(Contract.Result<DoubleNode<T>>() != null);

            var doubleNode = new DoubleNode<T>(new MultiNodeData<T>(width), group.First().MinorLeaf)
            {
                Childs = group.Select(node => node.Childs.First()).ToList()
            };

            return doubleNode;
        }
    }
}