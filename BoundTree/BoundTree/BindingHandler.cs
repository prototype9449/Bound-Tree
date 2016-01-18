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
    public class BindingHandler<T> : IBindingHandler<T> where T : class, IID<T>, IEquatable<T>, new()
    {
        private readonly MultiTree<T> _mainTree;
        private readonly SingleTree<T> _minorTree;
        private readonly List<KeyValuePair<T, T>> _connections = new List<KeyValuePair<T, T>>();

        public BindingHandler(MultiTree<T> mainTree, SingleTree<T> minorTree)
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

        public bool HandleBinding(MultiNode<T> mainSingleNode, SingleNode<T> minorSingleNode)
        {
            Contract.Requires(mainSingleNode != null);
            Contract.Requires(minorSingleNode != null); 

            if (!IsValidConnection(mainSingleNode, minorSingleNode))
                return false;

            if (_connections.Exists(pair => pair.Key == mainSingleNode.Id || pair.Value == minorSingleNode.Id))
                return false;

            _connections.Add(new KeyValuePair<T, T>(mainSingleNode.Id, minorSingleNode.Id));
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

        private bool IsValidConnection(MultiNode<T> first, SingleNode<T> second)
        {
            Contract.Requires(first != null);
            Contract.Requires(second != null);

            foreach (var connection in _connections)
            {
                if (GetRelationKind(connection.Key, first.Id, _mainTree) != GetRelationKind(connection.Value, second.Id, _minorTree))
                    return false;
            }

            return true;
        }

        private RelationKind GetRelationKind(T firstId, T secondId, dynamic tree)
        {
            Contract.Requires(firstId != null);
            Contract.Requires(secondId != null);

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
