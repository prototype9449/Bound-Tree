using System.Collections.Generic;
using System.Linq;
using BoundTree.Nodes;

namespace BoundTree.Helpers
{
    public class TreeStabilizer
    {
        public KeyValuePair<Tree, Tree> GetBalancedTrees(Tree mainTree, Tree minorTree, BindingHandler bindingHelper)
        {
            var mainVertexes = mainTree.ToList().Select(node => node.Identificator).ToList();
            var minorVertexes = minorTree.ToList().Select(node => node.Identificator).ToList();

            var mainStackBoundVertex = new Stack<Identificator>(bindingHelper.BoundNodes.Select(pair => pair.Key).Reverse());
            var minorStackBoundVertex = new Stack<Identificator>(bindingHelper.BoundNodes.Select(pair => pair.Value).Reverse());

            var mainLastIndex = 0;
            var minorLastIndex = 0;

            while (mainStackBoundVertex.Count!=0)
            {
                var mainBoundVertex = mainStackBoundVertex.Pop();
                var minorBoundVertex = minorStackBoundVertex.Pop();
                mainLastIndex += mainVertexes.Skip(mainLastIndex).ToList().IndexOf(mainBoundVertex);
                minorLastIndex += minorVertexes.Skip(minorLastIndex).ToList().IndexOf(minorBoundVertex);

                var difference = mainLastIndex - minorLastIndex;
                if (difference > 0)
                {
                    AddUselessIds(minorVertexes, difference, minorBoundVertex);
                }
                if (difference < 0)
                {
                    AddUselessIds(mainVertexes, difference, mainBoundVertex);
                }
            }
            var newMainTree = BuildTree(mainVertexes, mainTree); 
            var newMinorTree = BuildTree(minorVertexes, minorTree);
            
            return new KeyValuePair<Tree, Tree>(newMainTree, newMinorTree);

        }

        private void AddUselessIds(List<Identificator> ids, int difference, Identificator vertex)
        {
            var previousIndex = ids.IndexOf(vertex) - 1;
            var commonRoot = vertex.GetCommonRoot(ids[previousIndex]);
            commonRoot.OrderIds.Add(0);
            var uselessId = new Identificator(commonRoot.OrderIds);
            for (var i = 0; i < difference; i++)
            {
                ids.Insert(ids.IndexOf(vertex), uselessId);
            }
        }

        private Tree BuildTree(List<Identificator> ids, Tree tree)
        {
            var rootNode = GetNodeById(tree, ids.First());
            var nodes = new List<Node>();
            var firstNode = GetNodeById(tree, ids[1]);
            nodes.Add(firstNode);

            foreach (var id in ids.Skip(2))
            {
                var currentNode = GetNodeById(tree, id);
                if (nodes.Last().Add(currentNode)) continue;

                nodes.Add(currentNode);
            }
            rootNode.Nodes = nodes;
            var resultTree = new Tree(rootNode);
            return resultTree;
        }

        private Node GetNodeById(Tree tree, Identificator id)
        {
            var node = tree.GetNewInstanceById(id);
            if (node == null)
            {
                return new EmptyNode(id, tree.Root.BindingHandler);
            }

            return node;
        }
    }
}
