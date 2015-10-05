using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BoundTree
{
    [Serializable]
    public class Tree
    {
        public Node Root { get; set; }

        public Tree(Node root)
        {
            Root = root;
            Root.SetDeep(-1);
        }
       

        public Node GetById(int id)
        {
            var queue = new Queue<Node>();
            queue.Enqueue(Root);
            while (queue.Count != 0)
            {
                if (queue.Peek().Id == id)
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

        public Node GetParent(int id)
        {
            var queue = new Stack<KeyValuePair<Node, int>>();
            queue.Push(new KeyValuePair<Node, int>(Root, -1));
            while (queue.Count != 0)
            {
                if (queue.Peek().Key.Id == id)
                {
                    if (queue.Peek().Value == -1) 
                        return null;

                    return GetById(queue.Peek().Value);
                }
                var parent = queue.Pop().Key;
                foreach (var node in parent.Nodes)
                {
                    queue.Push(new KeyValuePair<Node, int>(node, parent.Id));
                }
            }

            return null;
        }

//        public Node GetNewInstanceById(int id)
//        {
//            return GetById(id).GetNewInstance();
//        }

        public List<Node> ToList()
        {
            var nodes = new List<Node>();
            RecursiveFillNodes(Root, nodes);
            return nodes;
        }

        private void RecursiveFillNodes(Node root, List<Node> nodes)
        {
            nodes.Add(root);

            if (root.Nodes.Count == 0) return;

            foreach (var node in root.Nodes)
            {
                RecursiveFillNodes(node, nodes);
            }
        }


        public Tree Clone()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (Tree)formatter.Deserialize(stream);
            }
        }
    }
}
