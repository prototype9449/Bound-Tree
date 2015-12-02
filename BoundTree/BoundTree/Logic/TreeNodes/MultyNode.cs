using System;
using System.Collections.Generic;
using BoundTree.Logic.NodeData;

namespace BoundTree.Logic.TreeNodes
{
    public class MultyNode<T> where T : new()
    {
        public MultyNodeData<T> MultyNodeData { get; set; }
        public List<MultyNode<T>> Childs { get; set; }

        public LogicLevel LogicLevel
        {
            get
            {
                return MultyNodeData.LogicLevel;
            }
        }

        public int Depth
        {
            get { return MultyNodeData.Depth; }
            set { MultyNodeData.Depth = value; }
        }

        public T Id
        {
            get { return MultyNodeData.Id; }
        }

        public Type NodeType
        {
            get
            {
                return MultyNodeData.GetType();
            }
        }

        public bool CanContain(MultyNodeData<T> multyNodeData)
        {
            return MultyNodeData.CanContain(multyNodeData);
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