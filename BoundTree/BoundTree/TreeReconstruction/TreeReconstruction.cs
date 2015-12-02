using System;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Logic;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;

namespace BoundTree.TreeReconstruction
{
    public class TreeReconstruction<T> where T : class, IEquatable<T>, new()
    {
        private readonly SingleTree<T> _mainTree;
        private readonly SingleTree<T> _minorTree;
        public readonly BindingHandler<T> _bindingHandler;

        public TreeReconstruction(BindContoller<T> bindContoller)
        {
            Contract.Requires(bindContoller != null);

            _minorTree = bindContoller.MinorSingleTree;
            _mainTree = bindContoller.MainSingleTree;
            _bindingHandler = bindContoller.Handler;
        }

        public DoubleNode<T> GetFilledTree()
        {
            Contract.Ensures(Contract.Result<DoubleNode<T>>() != null);

            var clonedMainTree = _mainTree.Clone();
            var connections = _bindingHandler.Connections
                .ToDictionary(pair => pair.Key, pair => _minorTree.GetById(pair.Value));

            var doubleNode = new ConnectionReconstruction<T>().GetDoubleNodeWithConnections(clonedMainTree, connections);
            new VirtualNodeReconstruction<T>(_minorTree).Reconstruct(doubleNode);
            new PostReconstruction<T>(_minorTree).Reconstruct(doubleNode);
            new ConnectionKindReconstruction<T>().Reconstruct(doubleNode);
            doubleNode.RecalculateDeep();
            return doubleNode;
        }
    }
}