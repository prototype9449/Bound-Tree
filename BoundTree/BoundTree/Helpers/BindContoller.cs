using System;

namespace BoundTree.Helpers
{
    public class BindContoller<T> where T : class, IEquatable<T>
    {
        public Tree<T> MainTree { get; private set; }
        public Tree<T> MinorTree { get; private set; }

        public BindContoller(Tree<T> mainTree, Tree<T> minorTree)
        {
            MainTree = mainTree;
            MinorTree = minorTree;
        }

        public bool Bind(T mainId, T minorId)
        {
            var mainNode = MainTree.GetById(mainId);
            var minorNode = MinorTree.GetById(minorId);

            if (mainNode == null || minorNode == null)
                return false;

            mainNode.BindWith(minorNode);
            return true;
        }

        public void RemoveConnection(T main)
        {
            MainTree.Root.BindingHandler.RemoveConnection(main);
        }
    }
}