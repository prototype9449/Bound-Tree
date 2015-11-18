using System;
using BoundTree.Logic;
using System.Collections.Generic;

namespace BoundTree.Helpers
{
    public class BindContoller<T> where T : class, IEquatable<T>, new()
    {
        public SingleTree<T> MainSingleTree { get; private set; }
        public SingleTree<T> MinorSingleTree { get; private set; }
        public BindingHandler<T> Handler { get; private set; }


        public BindContoller(SingleTree<T> mainSingleTree, SingleTree<T> minorSingleTree)
        {
            MainSingleTree = mainSingleTree;
            MinorSingleTree = minorSingleTree;
            Handler = new BindingHandler<T>(mainSingleTree, minorSingleTree);
            
            Bind(mainSingleTree.Root.Node.Id, minorSingleTree.Root.Node.Id);
        }

        public bool ClearConnection()
        {
            return Handler.ClearConnections();
        }

        public bool Bind(T mainId, T minorId)
        {
            var mainNode = MainSingleTree.GetById(mainId);
            var minorNode = MinorSingleTree.GetById(minorId);

            if (mainNode == null || minorNode == null)
                return false;

            if (mainNode.Node.NodeInfo.GetType() == minorNode.Node.NodeInfo.GetType())
            {
                return Handler.HandleBinding(mainNode, minorNode);
            }

            return false;
        }

        public bool RemoveConnection(T main)
        {
            return Handler.RemoveConnection(main);
        }
    }
}