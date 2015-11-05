using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BoundTree
{
    [Serializable]
    public class SingleTree<T> where T : class, IEquatable<T>, new()
    {
        public SingleNode<T> Root { get; set; }

        public SingleTree(SingleNode<T> root)
        {
            Root = root;
            Root.SetDeep(-1);
        }

        public SingleNode<T> GetById(T id)
        {
            var queue = new Queue<SingleNode<T>>();
            queue.Enqueue(Root);
            while (queue.Count != 0)
            {
                if (queue.Peek().Node.Id.Equals(id))
                {
                    return queue.Peek();
                }

                foreach (var node in queue.Dequeue().Nodes)
                {
                    queue.Enqueue(node);
                }
            }

            return null;
        }

        public SingleNode<T> GetParent(T id)
        {
            var queue = new Stack<dynamic>();
            queue.Push(new { Node = Root, ParentId = -1});
            while (queue.Count != 0)
            {
                var current = queue.Pop();
                if (current.Node.Id.Equals(id))
                {
                    if (current.ParentId == -1)
                        return null;

                    return GetById(current.ParentId);
                }

                foreach (var node in current.Nodes)
                {
                    queue.Push(new { Node = node, ParentId = current.Node.Id });
                }
            }

            return null;
        }

        public SingleTree<T> Clone()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (SingleTree<T>) formatter.Deserialize(stream);
            }
        }
    }
}