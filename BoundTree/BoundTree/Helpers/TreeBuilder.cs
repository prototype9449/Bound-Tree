using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundTree.Nodes;

namespace BoundTree.Helpers
{
    public class TreeBuilder
    {
        private Tree _tree;
        public TreeBuilder(Tree tree)
        {
            _tree = tree;
        }
        public Tree BuildTree(List<Identificator> identificators)
        {

            var rootNode = _tree.Root.GetNewInstance();
            var nodes = new List<Node>();
            var firstNode = GetBuiltNode(identificators.First());
            nodes.Add(firstNode);
            
            foreach (var identificator in identificators.Skip(1))
            {
                var currentNode = _tree.GetNewInstanceById(identificator);
                if (nodes.Last().Add(currentNode)) continue;

                nodes.Add(GetBuiltNode(identificator));
            }
            rootNode.Nodes = nodes;
            var resultTree = new Tree(rootNode);
            return resultTree;
        }
            
        private Node GetBuiltNode(Identificator identificator)
        {
            var stack = new Stack<Identificator>();
            stack.Push(identificator);
            while (true)
            {
                var topElement = stack.Peek().GetRootUpToNesting(2);
                if (topElement == null) break;
                stack.Push(stack.Peek().GetRootUpToNesting(2));
            }
            var resultNode = _tree.GetNewInstanceById(stack.Peek());
            while (stack.Count != 0)
            {
                resultNode.Add(_tree.GetNewInstanceById(stack.Pop()));
            }
            return resultNode;
        }
    }
}
