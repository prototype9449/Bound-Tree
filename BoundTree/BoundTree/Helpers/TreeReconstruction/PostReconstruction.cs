using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.Logic;
using BoundTree.Logic.Nodes;

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
            var stack = new Stack<DoubleNode<T>>(new[] {doubleNode});
            var passedNodes = new HashSet<DoubleNode<T>>();

            while (stack.Any())
            {
                var current = stack.Pop();

                if(!current.Nodes.Any() || passedNodes.Contains(current)) continue;

                var descendants = current.Nodes.Select(node => GetRepairedNode(current, node));

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
                    Nodes = new List<DoubleNode<T>>(new[] {doubleAscendant})
                };
                isDone = true;
            }

            return new KeyValuePair<bool, DoubleNode<T>>(isDone, doubleAscendant);
        }
    }
}