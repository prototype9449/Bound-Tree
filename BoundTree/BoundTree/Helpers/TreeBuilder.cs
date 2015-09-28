using System.Collections.Generic;
using System.Linq;


namespace BoundTree.Helpers
{
    public class TreeBuilder
    {
        public Tree BuildTreeByEnds(Tree tree, List<Identificator> identificators)
        {
            var rootNode = tree.Root.GetNewInstance();
            var nodes = new List<Node>();
            var firstNode = GetBuiltNode(tree, identificators.First());
            nodes.Add(firstNode);
            
            foreach (var identificator in identificators.Skip(1))
            {
                var currentNode = tree.GetNewInstanceById(identificator);
                if (nodes.Last().Add(currentNode)) continue;

                nodes.Add(GetBuiltNode(tree, identificator));
            }
            rootNode.Nodes = nodes;
            return new Tree(rootNode);
        }
            
        private Node GetBuiltNode(Tree tree, Identificator identificator)
        {
            var stack = new Stack<Identificator>();
            stack.Push(identificator);
            while (stack.Peek().GetRootUpToNesting(2) != null)
            {
                stack.Push(stack.Peek().GetRootUpToNesting(2));
            }
            var resultNode = tree.GetNewInstanceById(stack.Peek());
            while (stack.Count != 0)
            {
                resultNode.Add(tree.GetNewInstanceById(stack.Pop()));
            }
            return resultNode;
        }
    }
}
