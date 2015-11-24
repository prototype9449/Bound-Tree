using System.Collections.Generic;
using BoundTree.Logic;

namespace Build.TestFramework.Logic
{
    public class SimpleDoubleNode
    {
        public SimpleDoubleNode(string mainLeafId, string minorLeafId, ConnectionKind connectionKind, int depth)
        {
            MainLeaf = mainLeafId;
            MinorLeaf = minorLeafId;
            ConnectionKind = connectionKind;
            Depth = depth;

            Nodes = new List<SimpleDoubleNode>();
        }

        public string MainLeaf { get; set; }
        public string MinorLeaf { get; set; }
        public int Depth { get; set; }
        public ConnectionKind ConnectionKind { get; set; }
        public List<SimpleDoubleNode> Nodes { get; set; }

        public void Add(SimpleDoubleNode simpleDoubleNode)
        {
            Nodes.Add(simpleDoubleNode);
        }
    }
}