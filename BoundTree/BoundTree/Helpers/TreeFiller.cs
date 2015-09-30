using System.Collections.Generic;
using System.Linq;

namespace BoundTree.Helpers
{
    public class TreeFiller
    {
        public Tree GetFilledTree(Tree mainTree, Tree minorTree, BindingHandler bindingHandler)
        {
            var tree = mainTree.Clone();
            var dictionary = bindingHandler.BoundNodes.ToDictionary(pair => pair.Key, pair => minorTree.GetById(pair.Value));

            ReplaceLeafs(tree, dictionary);

            return tree;
        }

        private void ReplaceLeafs(Tree tree, Dictionary<int, Node> dictionary)
        {
            var queue = new Queue<Node>();
            queue.Enqueue(tree.Root);
            while (queue.Count != 0)
            {
                var currentId = queue.Peek().Id;
                if (dictionary.ContainsKey(currentId))
                {
                    tree.GetById(currentId).NodeInfo = dictionary[currentId].NodeInfo;
                }

                foreach (var node in queue.Dequeue().Nodes)
                {
                    queue.Enqueue(node);
                }
            }
        }
    }
}