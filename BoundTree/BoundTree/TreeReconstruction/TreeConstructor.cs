using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Interfaces;
using BoundTree.Logic;
using BoundTree.Logic.NodeData;
using BoundTree.Logic.TreeNodes;

namespace BoundTree.TreeReconstruction
{
    public class TreeConstructor<T> where T : class, IID<T>, IEquatable<T>, new()
    {
        private readonly ConnectionContructor<T> _connectionConstructor = new ConnectionContructor<T>();

        public DoubleNode<T> GetFilledTree(BindContoller<T> bindContoller, IIdGenerator<T> mainIdGenerator)
        {
            Contract.Requires(bindContoller != null);
            Contract.Ensures(Contract.Result<DoubleNode<T>>() != null);

            var mainTree = bindContoller.MainMultiTree;
            var minorTree = bindContoller.MinorSingleTree;

            var clonedMainTree = mainTree.Clone();
            var connections = bindContoller.Handler.Connections
                .ToDictionary(pair => pair.Key, pair => minorTree.GetById(pair.Value));

            var doubleNode = _connectionConstructor.GetDoubleNodeWithConnections(clonedMainTree, connections);
            new VirtualNodeConstruction<T>(minorTree).Reconstruct(doubleNode);
            new PostTreeConstructor<T>(minorTree).Reconstruct(doubleNode);
            ReconstructConnections(doubleNode);
            doubleNode.RecalculateDeep();

            ReconstructIds(doubleNode, mainIdGenerator);
            return doubleNode;
        }

        private void ReconstructConnections(DoubleNode<T> doubleNode)
        {
            Contract.Requires(doubleNode != null);

            foreach (var node in doubleNode.ToList().Where(node => node.ConnectionKind == ConnectionKind.None))
            {
                if (node.ToList().Exists(item => item.ConnectionKind != ConnectionKind.None))
                {
                    node.ConnectionKind = ConnectionKind.Relative;
                }
            }
        }

        private void ReconstructIds(DoubleNode<T> doubleNode, IIdGenerator<T> idGenerator)
        {
            Contract.Requires(idGenerator != null);
            var doubleNodes = doubleNode.ToList();

            var allIds = new List<NodeData<T>>();

            foreach (var node in doubleNodes)
            {
                allIds.Add(node.MainLeaf.NodeData);   
                allIds.Add(node.MinorLeaf.NodeData);   
                node.MainLeaf.MinorDataNodes.ForEach(connectionData => allIds.Add(connectionData.NodeData));   
            }

            var emptyNodes = allIds.Where(node => node.IsEmpty() && node.Id.IsEmpty());
            emptyNodes.ForEach(node => node.Id = idGenerator.GetNewId());
        }
    }
}