using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Interfaces;
using BoundTree.Logic;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;

namespace BoundTree
{
    [Serializable]
    public class BindingHandler<T> : IBindingHandler<T> where T : class, IEquatable<T>, new()
    {
        private readonly SingleTree<T> _mainTree;
        private readonly SingleTree<T> _minorTree;
        private readonly List<KeyValuePair<T, T>> _connections = new List<KeyValuePair<T, T>>();

        public BindingHandler(SingleTree<T> mainTree, SingleTree<T> minorTree)
        {
            Contract.Requires(mainTree != null);
            Contract.Requires(minorTree != null);

            _mainTree = mainTree;
            _minorTree = minorTree;
        }

        public bool ClearConnections()
        {
            Contract.Ensures(_connections.Count == 1);

            if (_connections.Count > 1)
            {
                _connections.Clear();
                HandleBinding(_mainTree.Root, _minorTree.Root);
                return true;
            }

            return false;
        }

        public List<KeyValuePair<T, T>> Connections
        {
            get { return _connections.ToList(); }
        }

        public bool HandleBinding(SingleNode<T> mainSingleNode, SingleNode<T> minorSingleNode)
        {
            Contract.Requires(mainSingleNode != null);
            Contract.Requires(minorSingleNode != null); 

            if (!IsValidConnection(mainSingleNode, minorSingleNode))
                return false;

            if (_connections.Exists(pair => pair.Key == mainSingleNode.SingleNodeData.Id || pair.Value == minorSingleNode.SingleNodeData.Id))
                return false;

            _connections.Add(new KeyValuePair<T, T>(mainSingleNode.SingleNodeData.Id, minorSingleNode.SingleNodeData.Id));
            return true;
        }

        public bool RemoveConnection(T mainId)
        {
            Contract.Requires(mainId != null);

            if(_connections.Exists(pair => pair.Key.Equals(mainId)))
            {
                _connections.RemoveAll(pair => pair.Key.Equals(mainId));
                return true;
            }

            return false;

        }

        private bool IsValidConnection(SingleNode<T> first, SingleNode<T> second)
        {
            Contract.Requires(first != null);
            Contract.Requires(second != null);

            foreach (var connection in _connections)
            {
                if (GetRelationKind(connection.Key, first.SingleNodeData.Id, _mainTree) != GetRelationKind(connection.Value, second.SingleNodeData.Id, _minorTree))
                    return false;
            }

            return true;
        }

        private RelationKind GetRelationKind(T firstId, T secondId, SingleTree<T> tree)
        {
            Contract.Requires(firstId != null);
            Contract.Requires(secondId != null);
            Contract.Requires(tree != null);

            var descendantOfFirst = tree.GetById(firstId).GetById(secondId);
            if (descendantOfFirst != null)
                return RelationKind.Ascendant;

            var descendantOfSecond = tree.GetById(secondId).GetById(firstId);
            if (descendantOfSecond != null)
                return RelationKind.Descendant;

            return RelationKind.Nothing;
        }
    }

}
