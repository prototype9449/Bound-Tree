using System;

namespace BoundTree.Helpers
{
    public class BindContoller<T> where T : class, IEquatable<T>
    {
        public Tree<T> MainTree { get; private set; }
        public Tree<T> MinorTree { get; private set; }
        public BindingHelper BindingHelper { get; private set; }
        public BindingHandler<T> BindingHandler { get; private set; }

        public BindContoller(Tree<T> mainTree, Tree<T> minorTree)
        {
            MainTree = mainTree;
            MinorTree = minorTree;
            BindingHelper = new BindingHelper();
            BindingHandler = new BindingHandler<T>();
        }

        public bool Bind(T mainId, T minorId)
        {
            var mainNode = MainTree.GetById(mainId);
            var minorNode = MinorTree.GetById(minorId);

            if (mainNode == null || minorNode == null)
                return false;

            if (BindingHelper.Bind(mainNode.NodeInfo, minorNode.NodeInfo))
            {
                BindingHandler.HandleBinding(mainNode, minorNode);
            }

           return true;
        }

        public void RemoveConnection(T main)
        {
            BindingHandler.RemoveConnection(main);
        }
    }
}