using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.Logic;

namespace BoundTree.Helpers.TreeReconstruction
{
    public class VirtualNodeReconstruction<T> where T : class, IEquatable<T>, new()
    {
        private readonly SingleTree<T> _minorTree;

        public VirtualNodeReconstruction(SingleTree<T> minorTree)
        {
            _minorTree = minorTree;
        }

        public void Reconstruct(DoubleNode<T> doubleNode)
        {
            var stack = new Stack<DoubleNode<T>>(new[] { doubleNode });
            var passedNodes = new HashSet<DoubleNode<T>>();

            while (stack.Any())
            {
                var currentNode = stack.Peek();
                if (!currentNode.Nodes.Any())
                {
                    passedNodes.Add(currentNode);
                    stack.Pop();
                    continue;
                }

                if (!currentNode.Nodes.All(passedNodes.Contains))
                {
                    currentNode.Nodes.ForEach(stack.Push);
                    continue;
                }

                RepairNode(currentNode);
                passedNodes.Add(stack.Pop());
            }
        }


        private void RepairNode(DoubleNode<T> doubleNode)
        {
            Node<T> commonParent = GetMostCommonParent(doubleNode.Nodes);

            if (commonParent.IsEmpty()) return;

            while (commonParent.LogicLevel > doubleNode.LogicLevel)
            {
                commonParent = GetMostCommonParent(commonParent);
            }

            if (commonParent.LogicLevel == doubleNode.LogicLevel && commonParent.NodeType == doubleNode.MainLeaf.NodeType)
            {
                doubleNode.MinorLeaf = commonParent;
                return;
            }

            if (commonParent.LogicLevel < doubleNode.LogicLevel)
            {
                doubleNode.Shadow = commonParent;
                return;
            }

            while (commonParent.LogicLevel >= doubleNode.LogicLevel)
            {
                commonParent = GetMostCommonParent(commonParent);
            }

            doubleNode.Shadow = commonParent;

            CleanUselessNodes(doubleNode, commonParent);
        }

        private void CleanUselessNodes(DoubleNode<T> doubleNode, Node<T> comparedNode)
        {
            var descendants = doubleNode.ToList();
            foreach (var descendant in descendants)
            {
                descendant.Shadow = null;
                var identicalNodes = descendants.FindAll(item => item.MinorLeaf == descendant.MinorLeaf);
                if (identicalNodes.Count > 1)
                {
                    identicalNodes.ForEach(item => item.MinorLeaf = new Node<T>());
                }
            }

            var tooHighLogicNodes = descendants.FindAll(item => item.LogicLevel < comparedNode.LogicLevel);
            tooHighLogicNodes.ForEach(item => item.MinorLeaf = new Node<T>());

            var tooHighDeepNodes = descendants
                .FindAll(item => item.LogicLevel == comparedNode.LogicLevel)
                .FindAll(item => item.Deep > comparedNode.Deep);

            tooHighDeepNodes.ForEach(item => item.MinorLeaf = new Node<T>());
        }

        private Node<T> GetMostCommonParent(Node<T> node)
        {
            if (node.LogicLevel == 0)
                return node;

            return _minorTree.GetParent(node.Id).Node;
        }

        private Node<T> GetMostCommonParent(IList<DoubleNode<T>> doubleNodes)
        {
            var notEmptyNodes = doubleNodes.Where(doubleNode => doubleNode.GetMinorValue() != null).ToList();

            if (!notEmptyNodes.Any())
            {
                return new Node<T>();
            }

            if (notEmptyNodes.Count() == 1)
            {
                var doubleNode = notEmptyNodes.First();
                if (doubleNode.Shadow != null && doubleNode.IsMinorEmpty())
                {
                    return doubleNode.Shadow;
                }
                //if node equals to Root
                if (doubleNode.LogicLevel == 0)
                {
                    return doubleNode.MinorLeaf;
                }

                return _minorTree.GetParent(doubleNode.MinorLeaf.Id).Node;
            }

            var routes = GetRoutes(notEmptyNodes);

            var minLength = routes.Min(nodes => nodes.Count);
            for (int i = 0; i < minLength; i++)
            {
                var areDifferent = routes
                    .Select(nodes => nodes[i])
                    .Any(node => node.Id != routes.First()[i].Id);

                if (areDifferent)
                {
                    return routes.First()[i - 1];
                }
            }

            return routes.First()[minLength - 1];
        }

        private List<List<Node<T>>> GetRoutes(List<DoubleNode<T>> notEmptyNodes)
        {
            var setRouts = new List<List<Node<T>>>();
            foreach (var doubleNode in notEmptyNodes)
            {
                var route = new List<Node<T>>();
                var minorNodeId = doubleNode.IsMinorEmpty()
                    ? doubleNode.Shadow.Id
                    : doubleNode.MinorLeaf.Id;

                var parentNode = _minorTree.GetParent(minorNodeId);

                while (parentNode != null)
                {
                    route.Add(parentNode.Node);
                    parentNode = _minorTree.GetParent(parentNode.Node.Id);
                }

                route.Reverse();

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