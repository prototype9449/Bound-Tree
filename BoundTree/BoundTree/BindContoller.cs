using System;
using System.Diagnostics.Contracts;
using BoundTree.Logic.Trees;

namespace BoundTree
{
    public class BindContoller<T> where T : class, IEquatable<T>, new()
    {
        public MultiTree<T> MainMultiTree { get; private set; }
        public SingleTree<T> MinorSingleTree { get; private set; }
        public BindingHandler<T> Handler { get; private set; }

        public BindContoller(MultiTree<T> mainSingleTree, SingleTree<T> minorSingleTree)
        {
            Contract.Requires(mainSingleTree != null);
            Contract.Requires(minorSingleTree != null);

            MainMultiTree = mainSingleTree;
            MinorSingleTree = minorSingleTree;
            Handler = new BindingHandler<T>(mainSingleTree, minorSingleTree);
            
            Bind(mainSingleTree.Root.Id, minorSingleTree.Root.Id);
        }

        public bool RemoveAllConnections()
        {
            return Handler.ClearConnections();
        }

        public bool Bind(T mainId, T minorId)
        {
            Contract.Requires(mainId != null);
            Contract.Requires(minorId != null);

            var mainNode = MainMultiTree.GetById(mainId);
            var minorNode = MinorSingleTree.GetById(minorId);

            if (mainNode == null || minorNode == null)
                return false;

            if (mainNode.NodeType == minorNode.NodeType)
            {
                return Handler.HandleBinding(mainNode, minorNode);
            }

            return false;
        }

        public bool RemoveConnection(T main)
        {
            Contract.Requires(main != null);

            return Handler.RemoveConnection(main);
        }
    }
}