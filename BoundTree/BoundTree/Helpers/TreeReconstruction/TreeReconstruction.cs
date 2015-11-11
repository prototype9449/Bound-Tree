using System;
using System.Linq;
using BoundTree.Logic;

namespace BoundTree.Helpers.TreeReconstruction
{
    public class TreeReconstruction<T> where T : class, IEquatable<T>, new()
    {
        private readonly SingleTree<T> _mainTree;
        private readonly SingleTree<T> _minorTree;
        public readonly BindingHandler<T> _bindingHandler;

        public TreeReconstruction(BindContoller<T> bindContoller)
        {
            _minorTree = bindContoller.MinorSingleTree;
            _mainTree = bindContoller.MainSingleTree;
            _bindingHandler = bindContoller.BindingHandler;
        }

        public DoubleNode<T> GetFilledTree()
        {
            var clonedMainTree = _mainTree.Clone();
            var connections = _bindingHandler.Connections
                .ToDictionary(pair => pair.Key, pair => _minorTree.GetById(pair.Value));

            var doubleNode = new ConnectionReconstruction<T>().GetDoubleNodeWithConnections(clonedMainTree, connections);
            new VirtualNodeReconstruction<T>(_minorTree).Reconstruct(doubleNode);
            new PostReconstruction<T>(_minorTree).Reconstruct(doubleNode);
            doubleNode.RecalculateDeep();
            return doubleNode;
        }
    }
}