using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using BoundTree.Interfaces;
using BoundTree.Logic.TreeNodes;

namespace BoundTree.Logic.Trees
{
    [Serializable]
    public class MultiTree<T> where T : class, IId<T>, IEquatable<T>, new()
    {
        
        public MultiTree(SingleNode<T> root, NodeInfoFactory nodeInfoFactory)
        {
            Contract.Requires(root != null);

            Root = new MultiNode<T>(root, nodeInfoFactory);
            Root.RecalculateDeep();
        }

        public MultiTree(SingleTree<T> singleTree, NodeInfoFactory nodeInfoFactory) : this(singleTree.Root, nodeInfoFactory)
        {
            Contract.Requires(singleTree != null);
        }

        public MultiTree(MultiTree<T> multiTree)
        {
            Contract.Requires(multiTree != null);
            Contract.Requires(multiTree.Root != null);

            Root = multiTree.Root;
            Root.RecalculateDeep();
        }

        public MultiTree(MultiNode<T> root)
        {
            Contract.Requires(root != null);

            Root = root;
           
            Root.RecalculateDeep();
        }

        public MultiNode<T> Root { get; private set; }

        public MultiNode<T> GetById(T id)
        {
            Contract.Requires(id != null);

            return Root.GetById(id);
        }

        public List<MultiNode<T>> ToList()
        {
            Contract.Requires(Root != null);

            return Root.ToList();
        }

        public MultiTree<T> Clone()
        {
            Contract.Ensures(Contract.Result<MultiTree<T>>() != null);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (MultiTree<T>)formatter.Deserialize(stream);
            }
        }
    }
}