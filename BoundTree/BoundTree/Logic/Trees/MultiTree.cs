using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using BoundTree.Logic.TreeNodes;

namespace BoundTree.Logic.Trees
{
    public class MultiTree<T> where T : class, IEquatable<T>, new()
    {
        public MultiTree(SingleNode<T> root)
        {
            Root = new MultiNode<T>(root);
        }

        public MultiTree(MultiNode<T> root)
        {
            Contract.Requires(root != null);

            Root = root;
        }

        public MultiNode<T> Root { get; private set; }

        public MultiTree(SingleTree<T> singleTree)
        {
            
        }

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