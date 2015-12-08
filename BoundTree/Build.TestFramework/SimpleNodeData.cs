using BoundTree.Logic;

namespace Build.TestFramework
{
    public class SimpleNodeData
    {
        public SimpleNodeData(ConnectionKind connectionKind, string id)
        {
            ConnectionKind = connectionKind;
            Id = id;
        }

        public string Id { get; set; }
        public ConnectionKind ConnectionKind { get; set; }

    }
}