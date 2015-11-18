using System;
using System.Collections.Generic;
using BoundTree.Interfaces;
using BoundTree.Logic;

namespace BoundTree.Helpers
{
    [Serializable]
    public class BindingHandler<T> : IBindingHandler<T> where T : class, IEquatable<T>, new()
    {
        private readonly SingleTree<T> _mainTree;
        private readonly SingleTree<T> _minorTree;
        private readonly List<KeyValuePair<T, T>> _connections = new List<KeyValuePair<T, T>>();

        public BindingHandler(SingleTree<T> mainTree, SingleTree<T> minorTree)
        {
            _mainTree = mainTree;
            _minorTree = minorTree;
        }

        public List<KeyValuePair<T, T>> Connections
        {
            get { return _connections; }
        }

        public bool HandleBinding(SingleNode<T> mainSingleNode, SingleNode<T> minorSingleNode)
        {
            if (!IsValidConnection(mainSingleNode, minorSingleNode))
                return false;

            if (Connections.Exists(pair => pair.Key == mainSingleNode.Node.Id || pair.Value == minorSingleNode.Node.Id))
                return false;

            Connections.Add(new KeyValuePair<T, T>(mainSingleNode.Node.Id, minorSingleNode.Node.Id));
            return true;
        }

        public void RemoveConnection(T mainId)
        {
            _connections.RemoveAll(pair => pair.Key.Equals(mainId));
        }

        private bool IsValidConnection(SingleNode<T> first, SingleNode<T> second)
        {
            foreach (var connection in Connections)
            {
                if (GetRelationKind(connection.Key, first.Node.Id, _mainTree) != GetRelationKind(connection.Value, second.Node.Id, _minorTree))
                    return false;
            }

            return true;
        }

        private RelationKind GetRelationKind(T firstId, T secondId, SingleTree<T> tree)
        {
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
