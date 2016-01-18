using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using BoundTree.Interfaces;
using BoundTree.Logic.Nodes;
using BoundTree.Logic.TreeNodes;

namespace BoundTree.Logic.Trees
{
    [Serializable]
    public class SingleTree<T> where T : class, IID<T>, IEquatable<T>, new()
    {
        public SingleTree(SingleNode<T> root)
        {
            Contract.Requires(root != null);

            Root = root;
            Root.RecalculateDeep();
        }

        public SingleNode<T> Root { get; private set; }

        public SingleNode<T> GetById(T id)
        {
            Contract.Requires(id != null);

            return Root.GetById(id);
        }

        private Stack<Ttype> GetStack<Ttype>(Ttype item)
        {
            return new Stack<Ttype>(new[] { item });
        }

        public SingleNode<T> GetParent(T id)
        {
            Contract.Requires(id != null);

            var stack = GetStack(new { SingleNode = Root, ParentId = new T() });
            while (stack.Count != 0)
            {
                var current = stack.Pop();
                if (current.SingleNode.SingleNodeData.Id.Equals(id))
                {
                    if (current.ParentId.Equals(new T()))
                        return null;

                    return GetById(current.ParentId);
                }

                foreach (var node in current.SingleNode.Childs)
                {
                    stack.Push(new { SingleNode = node, ParentId = current.SingleNode.SingleNodeData.Id });
                }
            }

            return null;
        }

        public List<SingleNode<T>> ToList()
        {
            Contract.Requires(Root != null);
            Contract.Ensures(Contract.Result<List<SingleNode<T>>>().Any());

            return Root.ToList();
        }

        public SingleTree<T> Clone()
        {
            Contract.Ensures(Contract.Result<SingleTree<T>>() != null);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (SingleTree<T>)formatter.Deserialize(stream);
            }
        }
    }
}