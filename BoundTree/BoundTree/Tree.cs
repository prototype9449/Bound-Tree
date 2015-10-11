using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BoundTree
{
    [Serializable]
    public class Tree<T> where T : class, IEquatable<T>
    {
        public Node<T> Root { get; set; }

        public Tree(Node<T> root)
        {
            Root = root;
            Root.SetDeep(-1);
        }


        public Node<T> GetById(T id)
        {
            var queue = new Queue<Node<T>>();
            queue.Enqueue(Root);
            while (queue.Count != 0)
            {
                if (queue.Peek().Id.Equals(id))
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

        public Node<T> GetParent(T id)
        {
            var queue = new Stack<KeyValuePair<Node<T>, T>>();
            queue.Push(new KeyValuePair<Node<T>, T>(Root, null));
            while (queue.Count != 0)
            {
                if (queue.Peek().Key.Id.Equals(id))
                {
                    if (queue.Peek().Value == null)
                        return null;

                    return GetById(queue.Peek().Value);
                }
                var parent = queue.Pop().Key;
                foreach (var node in parent.Nodes)
                {
                    queue.Push(new KeyValuePair<Node<T>, T>(node, parent.Id));
                }
            }

            return null;
        }

        public List<Node<T>> ToList()
        {
            var nodes = new List<Node<T>>();
            RecursiveFillNodes(Root, nodes);
            return nodes;
        }

        private void RecursiveFillNodes(Node<T> root, List<Node<T>> nodes)
        {
            nodes.Add(root);

            if (root.Nodes.Count == 0) return;

            foreach (var node in root.Nodes)
            {
                RecursiveFillNodes(node, nodes);
            }
        }


        public Tree<T> Clone()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (Tree<T>)formatter.Deserialize(stream);
            }
        }
    }
}
