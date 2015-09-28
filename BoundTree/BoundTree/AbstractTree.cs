using System;
using System.Collections;
using System.Collections.Generic;

namespace BoundTree
{
    public abstract class AbstractTree
    {
       
        public Node Root { get; set; }

        protected AbstractTree(Node root)
        {
            Root = root;
        }

        public void Add(Node node)
        {
            Root.Add(node);
        }
        public Node GetById(Identificator identificator)
        {
            return Root.GetNodeById(identificator);
        }

        public Node GetNewInstanceById(Identificator identificator)
        {
            return Root.GetNewInstanceById(identificator);
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
            if (root.Nodes.Count != 0)
            {
                foreach (var node in root.Nodes)
                {
                    RecursiveFillNodes(node, nodes);
                }
            }
        }

    }
}
