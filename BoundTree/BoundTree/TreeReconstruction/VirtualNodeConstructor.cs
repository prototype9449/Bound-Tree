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
    public class VirtualNodeConstruction<T> where T : class, IId<T>, IEquatable<T>, new()
    {
        private readonly SingleTree<T> _minorTree;
        private readonly NodeInfoFactory _nodeInfoFactory;

        public VirtualNodeConstruction(SingleTree<T> minorTree, NodeInfoFactory nodeInfoFactory)
        {
            Contract.Requires(minorTree != null);

            _minorTree = minorTree;
            _nodeInfoFactory = nodeInfoFactory;
        }

        public void Reconstruct(DoubleNode<T> doubleNode)
        {
            Contract.Requires(doubleNode != null);

            var stack = new Stack<DoubleNode<T>>(new[] { doubleNode });
            var passedNodes = new HashSet<DoubleNode<T>>();

            while (stack.Any())
            {
                var currentNode = stack.Peek();
                if (!currentNode.Childs.Any())
                {
                    passedNodes.Add(currentNode);
                    stack.Pop();
                    continue;
                }

                if (!currentNode.Childs.All(passedNodes.Contains))
                {
                    currentNode.Childs.ForEach(stack.Push);
                    continue;
                }

                RepairNode(currentNode);
                passedNodes.Add(stack.Pop());
            }
        }


        private void RepairNode(DoubleNode<T> doubleNode)
        {
            Contract.Requires(doubleNode != null);

            SingleNodeData<T> commonParent = GetMostCommonParent(doubleNode.Childs);

            if (commonParent.IsEmpty())
            {
                CleanIdenticalNodes(doubleNode);
                return;
            }

            while (commonParent.LogicLevel > doubleNode.LogicLevel)
            {
                commonParent = GetMostCommonParent(commonParent);
            }

            if (commonParent.LogicLevel == doubleNode.LogicLevel)
            {
                doubleNode.MinorLeaf = commonParent;
                CleanUselessNodes(doubleNode, commonParent);
                return;
            }

            if (commonParent.LogicLevel < doubleNode.LogicLevel)
            {
                CleanUselessNodes(doubleNode, commonParent);
                doubleNode.Shadow = commonParent;
            }
        }

        private void CleanUselessNodes(DoubleNode<T> doubleNode, SingleNodeData<T> comparedNodeData)
        {
            Contract.Requires(doubleNode != null);
            Contract.Requires(comparedNodeData != null);

            CleanIdenticalNodes(doubleNode);

            var tooHighLogicNodes = doubleNode.ToList().FindAll(item => item.LogicLevel < comparedNodeData.LogicLevel);
            tooHighLogicNodes.ForEach(item => item.MinorLeaf = new SingleNodeData<T>(_nodeInfoFactory));

            //            var tooHighDeepNodes = descendants
            //                .FindAll(item => item.LogicLevel == comparedNodeData.LogicLevel)
            //                .FindAll(item => item.Deep > comparedNodeData.Depth);
            //
            //            tooHighDeepNodes.ForEach(item => item.MinorLeaf = new NodeData<T>());
        }

        private void CleanIdenticalNodes(DoubleNode<T> doubleNode)
        {
            Contract.Requires(doubleNode != null);

            var descendants = doubleNode.ToList();
            foreach (var descendant in descendants)
            {
                descendant.Shadow = null;
                var identicalNodes = descendants.FindAll(item => item.MinorLeaf == descendant.MinorLeaf);
                if (identicalNodes.Count > 1)
                {
                    identicalNodes.ForEach(item => item.MinorLeaf = new SingleNodeData<T>(_nodeInfoFactory));
                }
            }
        }

        private SingleNodeData<T> GetMostCommonParent(SingleNodeData<T> nodeData)
        {
            Contract.Requires(nodeData != null);

            if (nodeData.LogicLevel == LogicLevel.ZeroLevel)
                return nodeData;

            return _minorTree.GetParent(nodeData.Id).SingleNodeData;
        }

        private SingleNodeData<T> GetMostCommonParent(IList<DoubleNode<T>> doubleNodes)
        {
            Contract.Requires(doubleNodes != null);
            Contract.Requires(doubleNodes.Any());
            Contract.Ensures(Contract.Result<SingleNodeData<T>>() != null);

            var notEmptyNodes = doubleNodes.Where(doubleNode => doubleNode.GetMinorValue() != null).ToList();

            if (!notEmptyNodes.Any())
            {
                return new SingleNodeData<T>(_nodeInfoFactory);
            }

            if (notEmptyNodes.Count() == 1)
            {
                var doubleNode = notEmptyNodes.First();
                if (doubleNode.Shadow != null && doubleNode.IsMinorEmpty())
                {
                    return doubleNode.Shadow;
                }
                //if NodeData equals to Root
                if (doubleNode.LogicLevel == LogicLevel.ZeroLevel)
                {
                    return doubleNode.MinorLeaf;
                }

                return _minorTree.GetParent(doubleNode.MinorLeaf.Id).SingleNodeData;
            }

            var routes = GetRoutes(notEmptyNodes);

            var minLength = routes.Min(nodes => nodes.Count);
            for (int i = 0; i < minLength; i++)
            {
                var areDifferent = routes
                    .Select(nodes => nodes[i])
                    .Any(node => node != routes.First()[i]);

                if (areDifferent)
                {
                    return routes.First()[i - 1];
                }
            }

            return routes.First()[minLength - 1];
        }

        private List<List<SingleNodeData<T>>> GetRoutes(List<DoubleNode<T>> notEmptyNodes)
        {
            Contract.Requires(notEmptyNodes != null);
            Contract.Requires(notEmptyNodes.Any());
            Contract.Ensures(Contract.Result<List<List<SingleNodeData<T>>>>() != null);
            Contract.Ensures(Contract.Result<List<List<SingleNodeData<T>>>>().Any());

            var setRouts = new List<List<SingleNodeData<T>>>();
            foreach (var doubleNode in notEmptyNodes)
            {
                var route = new List<SingleNodeData<T>>();

                SingleNode<T> parentNode;

                //Если minor не пустой то берём его родителя, иначе берём самого shadow, так как shadow выше по уровню чем Main
                if (!doubleNode.IsMinorEmpty())
                {
                    parentNode = _minorTree.GetParent(doubleNode.MinorLeaf.Id);
                }
                else
                {
                    parentNode = _minorTree.GetById(doubleNode.Shadow.Id);
                }

                while (parentNode != null)
                {
                    route.Add(parentNode.SingleNodeData);
                    parentNode = _minorTree.GetParent(parentNode.SingleNodeData.Id);
                }

                route.Reverse();
                route.Add(doubleNode.MinorLeaf);
                var childNode = doubleNode.GetLonelyChild();
                if (childNode == null)
                {
                    setRouts.Add(route);
                    continue;
                }

                do
                {
                    route.Add(childNode.MinorLeaf);
                } while ((childNode = childNode.GetLonelyChild()) != null);

                setRouts.Add(route);
            }
            return setRouts;
        }
    }
}