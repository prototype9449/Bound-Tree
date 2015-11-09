using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.Logic;
using BoundTree.NodeInfo;

namespace BoundTree.Helpers
{
    public class TreeFiller<T> where T : class, IEquatable<T>, new()
    {
        private readonly SingleTree<T> _mainTree;
        private readonly SingleTree<T> _minorTree;
        public readonly BindingHandler<T> _bindingHandler;

        public TreeFiller(BindContoller<T> bindContoller)
        {
            _minorTree = bindContoller.MinorSingleTree;
            _mainTree = bindContoller.MainSingleTree;
            _bindingHandler = bindContoller.BindingHandler;
        }

        public DoubleNode<T> GetFilledTree()
        {
            var clonedMainTree = _mainTree.Clone();
            var connections = _bindingHandler.BoundNodes
                .ToDictionary(pair => pair.Key, pair => _minorTree.GetById(pair.Value));

            var doubleNode = GetDoubleNode(clonedMainTree, connections);
            RestoreRestNodes(doubleNode);
            return doubleNode;
        }

        private DoubleNode<T> GetDoubleNode(SingleTree<T> mainTree, Dictionary<T, SingleNode<T>> connections)
        {
            var resultDoubleNode = new DoubleNode<T>(mainTree.Root);

            var root = new {node = mainTree.Root, doubleNode = resultDoubleNode};
            var queue = new Queue<dynamic>(new[] {root});

            while (queue.Any())
            {
                var current = queue.Dequeue();
                var mainCurrentId = current.doubleNode.MainLeaf.Id;

                if (connections.ContainsKey(mainCurrentId))
                {
                    current.doubleNode.MinorLeaf = connections[mainCurrentId].Node;
                    current.doubleNode.ConnectionKind = ConnectionKind.Strict;
                }
                else
                {
                    current.doubleNode.MinorLeaf = new Node<T>(new T(), -1, new EmptyNodeInfo());
                    current.doubleNode.ConnectionKind = ConnectionKind.None;
                }

                foreach (var node in current.node.Nodes)
                {
                    var doubleNode = new DoubleNode<T>(node);
                    queue.Enqueue(new {node, doubleNode});
                    current.doubleNode.Add(doubleNode);
                }
            }

            return resultDoubleNode;
        }

        private void RestoreRestNodes(DoubleNode<T> doubleNode)
        {
            var stack = new Stack<DoubleNode<T>>(new[] {doubleNode});
            var markedNodes = new HashSet<DoubleNode<T>>();

            while (stack.Any())
            {
                var currentNode = stack.Peek();
                if (!currentNode.Nodes.Any())
                {
                    markedNodes.Add(currentNode);
                    stack.Pop();
                    continue;
                }

                if (!currentNode.Nodes.All(markedNodes.Contains))
                {
                    currentNode.Nodes.ForEach(stack.Push);
                    continue;
                }

                RepairNode(currentNode);
                markedNodes.Add(stack.Pop());
            }
        }


        private void RepairNode(DoubleNode<T> doubleNode)
        {
            Node<T> commonParent;
            if (!doubleNode.MinorLeaf.IsEmpty())
            {
                commonParent = GetMostCommonParent(doubleNode.Nodes);
                CleanUselessNodes(doubleNode, commonParent);
                return;
            }

            commonParent = GetMostCommonParent(doubleNode.Nodes);
            if(commonParent.IsEmpty()) return;

            while (commonParent.LogicLevel < doubleNode.LogicLevel)
            {
                commonParent = GetMostCommonParent(commonParent);
            }

            if (commonParent.LogicLevel == doubleNode.LogicLevel && commonParent.NodeType == doubleNode.MainLeaf.NodeType)
            {
                doubleNode.MinorLeaf = commonParent;
                return;
            }

            if (commonParent.LogicLevel > doubleNode.LogicLevel)
            {
                doubleNode.Shadow = commonParent;
                return;
            }

            while (commonParent.LogicLevel <= doubleNode.LogicLevel)
            {
                commonParent = GetMostCommonParent(commonParent);
            }

            doubleNode.Shadow = commonParent;
        }

        private void CleanUselessNodes(DoubleNode<T> node, Node<T> comparedNode)
        {
            var descendants = node.ToList();
            foreach (var descendant in descendants)
            {
                descendant.Shadow = null;
                var identicalNodes = descendants.FindAll(item => item.MinorLeaf == descendant.MinorLeaf);
                if (identicalNodes.Count > 1)
                {
                    identicalNodes.ForEach(item => item.MinorLeaf = new Node<T>());
                }

                var tooHighLogicNodes = descendants.FindAll(item => descendant.LogicLevel < comparedNode.LogicLevel);
                tooHighLogicNodes.ForEach(item => item.MinorLeaf = new Node<T>());

                var tooHighDeepNodes = descendants
                    .FindAll(item => descendant.LogicLevel == item.LogicLevel)
                    .FindAll(item => descendant.Deep < item.Deep);

                tooHighDeepNodes.ForEach(item => item.MinorLeaf = new Node<T>());
            }
        }

        private Node<T> GetMostCommonParent(Node<T> node)
        {
            if (node.LogicLevel == 0)
                return node;

            return _minorTree.GetParent(node.Id).Node;
        }

        private Node<T> GetMostCommonParent(IList<DoubleNode<T>> doubleNodes)
        {
            var notEmptyNodes = doubleNodes.Where(node => node.GetMinorValue() != null);
            if (!notEmptyNodes.Any())
            {
                return new Node<T>();
            }

            if (notEmptyNodes.Count() == 1)
            {
                var doubleNode = notEmptyNodes.First();
                if (doubleNode.Shadow != null && doubleNode.MinorLeaf.IsEmpty())
                {
                    return doubleNode.Shadow;
                }
                //if node equals Root
                if (doubleNode.LogicLevel == 0)
                {
                    return doubleNode.MinorLeaf;
                }

                return _minorTree.GetParent(doubleNode.MinorLeaf.Id).Node;
            }
            
            List<List<Node<T>>> setRouts = new List<List<Node<T>>>();
            foreach (var doubleNode in doubleNodes)
            {
                var route = new List<Node<T>>();
                Node<T> parentNode;
                while ((parentNode = _minorTree.GetParent(doubleNode.MinorLeaf.Id).Node) != null)
                {
                    route.Add(parentNode);
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

            var minLength = setRouts.Min(nodes => nodes.Count);
            for (int i = 0; i < minLength; i++)
            {
                if (setRouts.Any(nodes => nodes[i].Id != nodes.First().Id))
                {
                    return setRouts.First()[i-1];
                }
            }

            return setRouts.First()[minLength - 1];
        }
    }
}