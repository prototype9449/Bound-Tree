using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace BoundTree.Logic.TreeNodes
{
    public class MultyNode<T> where T : new()
    {
        public Node<T> MaiNode { get; set; }
        public List<Node<T>> MinorNodes { get; set; }
        public List<MultyNode<T>> Childs { get; set; }

        public LogicLevel LogicLevel
        {
            get
            {
                Contract.Requires(!MaiNode.IsEmpty() || MinorNodes.Exists(node => !node.IsEmpty()));
                Contract.Ensures(Contract.Result<LogicLevel>() != null);

                if (MaiNode.IsEmpty())
                {
                    return MinorNodes.First(node => !node.IsEmpty()).LogicLevel;
                }

                return MaiNode.LogicLevel;
            }
        }

        public int Depth { get; set; }

        public T Id
        {
            get { return MaiNode.Id; }
        }

        public bool CanContain(SingleNode<T> singleNode)
        {
            return MaiNode.NodeInfo.CanContain(singleNode.Node.NodeInfo);
        }

        public MultyNode<T> GetById(T id)
        {
            var queue = new Queue<MultyNode<T>>();
            queue.Enqueue(this);
            while (queue.Count != 0)
            {
                if (queue.Peek().Id.Equals(id))
                {
                    return queue.Peek();
                }

                foreach (var node in queue.Dequeue().Childs)
                {
                    queue.Enqueue(node);
                }
            }

            return null;
        }
    }
}