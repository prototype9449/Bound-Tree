using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BoundTree.Logic
{
    [Serializable]
    public class SingleTree<T> where T : class, IEquatable<T>, new()
    {
        private SingleNode<T> _root;

        public SingleNode<T> Root
        {
            get { return _root; }
            set
            {
                _root = value;

                if (value != null)
                {
                    _root.RecalculateDeep();
                }
            }
        }

        public SingleTree(SingleNode<T> root)
        {
            Root = root;
        }

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
                if (current.SingleNode.Node.Id.Equals(id))
                {
                    if (current.ParentId.Equals(new T()))
                        return null;

                    return GetById(current.ParentId);
                }

                foreach (var node in current.SingleNode.Nodes)
                {
                    stack.Push(new { SingleNode = node, ParentId = current.SingleNode.Node.Id });
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