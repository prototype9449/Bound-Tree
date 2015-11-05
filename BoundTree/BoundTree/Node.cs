using BoundTree.NodeInfo;

namespace BoundTree
{
    public class Node<T>
    {
        public int Deep { get; internal set; }
        public INodeInfo NodeInfo { get; protected set; }
        public T Id { get; protected set; }

        public Node(T id, int deep, INodeInfo nodeInfo)
        {
            Id = id;
            Deep = deep;
            NodeInfo = nodeInfo;
        }
    }
}