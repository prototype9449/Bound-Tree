using System.Collections.Generic;

namespace BoundTree
{
    public class Tree
    {
        public Node Root { get; set; }

        protected Tree(Node root)
        {
            Root = root;
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

        public Node GetNewInstanceById(int id)
        {
            return GetById(id).GetNewInstance();
        }

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

    }
}
