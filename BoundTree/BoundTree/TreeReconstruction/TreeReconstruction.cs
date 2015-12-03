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
        private readonly MultiTree<T> _mainTree;
        private readonly SingleTree<T> _minorTree;
        public readonly BindingHandler<T> _bindingHandler;

        private readonly ConnectionContructor<T> _connectionConstructor = new ConnectionContructor<T>();
        private ConnectionKindConstructor<T> _connectionKindConstructor = new ConnectionKindConstructor<T>();

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

            var doubleNode = _connectionConstructor.GetDoubleNodeWithConnections(clonedMainTree, connections);
            new VirtualNodeConstruction<T>(_minorTree).Reconstruct(doubleNode);
            new PostReconstruction<T>(_minorTree).Reconstruct(doubleNode);
            _connectionKindConstructor.Reconstruct(doubleNode);
            doubleNode.RecalculateDeep();

            return doubleNode;
        }
    }
}