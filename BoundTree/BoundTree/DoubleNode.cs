using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundTree.Helpers;

namespace BoundTree
{
    public class DoubleNode<T> where T : class, IEquatable<T>
    {
        public Cortege<T> MainLeaf { get; set; }
        public Cortege<T> MinorLeaf { get; set; }

        public List<DoubleNode<T>> Nodes { get; set; }

        public DoubleNode()
        {
            MainLeaf = new Cortege<T>();
            MinorLeaf = new Cortege<T>();
            Nodes = new List<DoubleNode<T>>();
        }

        public DoubleNode(Cortege<T> mainLeaf, Cortege<T> minorLeaf) : this()
        {
            MainLeaf = mainLeaf;
            MinorLeaf = minorLeaf;
        }

        public DoubleNode(Cortege<T> mainLeaf) : this()
        {
            MainLeaf = mainLeaf;
        }

        public DoubleNode(Node<T> node) : this(new Cortege<T>(node)) { }

        public void Add(DoubleNode<T> dobuleNode)
        {
            Nodes.Add(dobuleNode);
        }
    }
}
