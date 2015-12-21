using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Logic;
using BoundTree.Logic.NodeData;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;

namespace BoundTree.TreeReconstruction
{
    public class TreeConstructor<T> where T : class, IEquatable<T>, new()
    {
        private readonly ConnectionContructor<T> _connectionConstructor = new ConnectionContructor<T>();

        public DoubleNode<T> GetFilledTree(BindContoller<T> bindContoller, IIdGenerator<T> mainIdGenerator, IIdGenerator<T> minorIdGenerator)
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

            var mainNodes = doubleNode.ToList().Select(node => node.MainLeaf.NodeData);
            var minorNodes = doubleNode.ToList().Select(node => node.MinorLeaf.NodeData);
            ReconstructIds(mainNodes, mainIdGenerator);
            ReconstructIds(minorNodes, minorIdGenerator);
            return doubleNode;
        }

        public void ReconstructConnections(DoubleNode<T> doubleNode)
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

        public void ReconstructIds(IEnumerable<NodeData<T>> nodes, IIdGenerator<T> idGenerator)
        {
            nodes.Where(node => node.IsEmpty()).ForEach(node => node.Id = idGenerator.GetNewId());
        }
    }

}