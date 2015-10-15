using System;

namespace BoundTree.Helpers
{
    public class BindContoller<T> where T : class, IEquatable<T>
    {
        private readonly Tree<T> _mainTree;
        private readonly Tree<T> _minorTree;
        
        public BindContoller(Tree<T> mainTree, Tree<T> minorTree)
        {
            _mainTree = mainTree;
            _minorTree = minorTree;
        }

        public bool Bind(T mainId, T minorId)
        {
            var mainNode = _mainTree.GetById(mainId);
            var minorNode = _minorTree.GetById(minorId);

            if (mainNode == null || minorNode == null)
                return false;

            mainNode.BindWith(minorNode);
            return true;
        }

        public void RemoveConnection(T main)
        {
            _mainTree.Root.BindingHandler.RemoveConnection(main);
        }
    }
}