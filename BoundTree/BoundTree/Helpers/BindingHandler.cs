using System;
using System.Collections.Generic;
using BoundTree.Interfaces;
using BoundTree.Logic;

namespace BoundTree.Helpers
{
    [Serializable]
    public class BindingHandler<T> : IBindingHandler<T> where T : class, IEquatable<T>,new()
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

        public void HandleBinding(SingleNode<T> mainSingleNode, SingleNode<T> minorSingleNode)
        {
            if (!IsValidConnection(mainSingleNode, minorSingleNode))
                return;

            Connections.Add(new KeyValuePair<T, T>(mainSingleNode.Node.Id, minorSingleNode.Node.Id));
        }

        public void RemoveConnection(T mainId)
        {
            _connections.RemoveAll(pair => pair.Key.Equals(mainId));
        }

        private bool IsValidConnection(SingleNode<T> first, SingleNode<T> second)
        {
            foreach (var connection in Connections)
            {
                if (GetRelationKind(connection.Key, first.Node.Id) != GetRelationKind(connection.Value, second.Node.Id))
                    return false;
            }

            return true;
        }

        private RelationKind GetRelationKind(T firstId, T secondId)
        {
            var descendantOfFirst = _mainTree.GetById(firstId).GetById(secondId);
            if (descendantOfFirst != null)
                return RelationKind.Ascendant;

            var descendantOfSecond = _minorTree.GetById(secondId).GetById(firstId);
            if (descendantOfSecond != null)
                return RelationKind.Descendant;

            return RelationKind.Nothing;
        }
    }

}
