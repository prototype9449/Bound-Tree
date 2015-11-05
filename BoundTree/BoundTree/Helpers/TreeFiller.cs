using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.NodeInfo;

namespace BoundTree.Helpers
{
    public class TreeFiller<T> where T : class, IEquatable<T>, new()
    {
        private readonly BindContoller<T> _bindContoller;

        public TreeFiller(BindContoller<T> bindContoller)
        {
            _bindContoller = bindContoller;
        }

        public DoubleNode<T> GetFilledTree()
        {
            var clonedMainTree = _bindContoller.MainSingleTree.Clone();
            var connections = _bindContoller.BindingHandler.BoundNodes
                .ToDictionary(pair => pair.Key, pair => _bindContoller.MinorSingleTree.GetById(pair.Value));

            var doubleNode = GetDoubleNode(clonedMainTree, connections);
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
                    current.doubleNode.MinorLeaf = new Node<T>(current.doubleNode.MainLeaf.Id, -1, new EmptyNodeInfo());
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
            DoubleNode<T> commonParent;
            if (doubleNode.MinorLeaf.NodeInfo.Type != "Empty")
            {
                commonParent = GetCommonMostParent(doubleNode.Nodes);
                CleanUselessNodes(doubleNode, commonParent);
                return;
            }

            commonParent = GetCommonMostParent(doubleNode.Nodes);

            while (commonParent.LogicLevel < doubleNode.LogicLevel)
            {
                commonParent = GetCommonMostParent(commonParent.Nodes);
            }

            if (commonParent.LogicLevel == doubleNode.LogicLevel && commonParent.Type == doubleNode.Type)
            {
                doubleNode.MinorLeaf = commonParent.MinorLeaf;
                return;
            }

            if (commonParent.LogicLevel > doubleNode.LogicLevel)
            {
                doubleNode.Shadow = commonParent.MinorLeaf;
                return;
            }

            while (commonParent.LogicLevel <= doubleNode.LogicLevel)
            {
                commonParent = GetCommonMostParent(commonParent.Nodes);
            }

            doubleNode.Shadow = commonParent.MinorLeaf;
        }

        private void CleanUselessNodes(DoubleNode<T> node, DoubleNode<T> comparedNode)
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

        private DoubleNode<T> GetCommonMostParent(IList<DoubleNode<T>> nodes)
        {
            
        }
    }
}