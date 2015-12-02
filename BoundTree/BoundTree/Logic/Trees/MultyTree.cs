using System;
using System.Diagnostics.Contracts;
using BoundTree.Logic.TreeNodes;

namespace BoundTree.Logic.Trees
{
    public class MultyTree<T> where T : class, IEquatable<T>, new()
    {
        public MultyTree(SingleNode<T> root)
        {
            Root = new MultyNode<T>();
        }

        public MultyNode<T> Root { get; private set; }

        public MultyTree(MultyNode<T> root)
        {
            Contract.Requires(root != null);

            Root = root;
        }

        public MultyTree(SingleTree<T> singleTree)
        {
            
        }

        public MultyNode<T> GetById(T id)
        {
            Contract.Requires(id != null);

            return Root.GetById(id);
        }
    }
}