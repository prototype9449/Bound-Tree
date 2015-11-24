using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using BoundTree.Logic;
using Build.TestFramework.Logic;

namespace Build.TestFramework
{
    public class SimpleDoubleNodeParser
    {
        private const char TabSeparator = '\t';

        public SimpleDoubleNode ParseDoubleNode(DoubleNode<StringId> doubleNode)
        {
            Contract.Requires(doubleNode != null);
            Contract.Ensures(Contract.Result<SimpleDoubleNode>() != null);

            var result = GetSimpleDoubleNode(doubleNode);

            var root = new { doubleNode = doubleNode, simpleDoubleNode = result };
            var queue = GetQueue(root);

            while (queue.Any())
            {
                var current = queue.Dequeue();
                foreach (var node in current.doubleNode.Nodes)
                {
                    var simpleDoubleNode = GetSimpleDoubleNode(node);
                    queue.Enqueue(new { doubleNode = node, simpleDoubleNode = simpleDoubleNode });
                    current.simpleDoubleNode.Add(simpleDoubleNode);
                }
            }

            return result;
        }

        private SimpleDoubleNode GetSimpleDoubleNode(DoubleNode<StringId> doubleNode)
        {
            Contract.Requires(doubleNode != null);
            Contract.Ensures(Contract.Result<SimpleDoubleNode>() != null);

            var mainLeafId = doubleNode.MainLeaf.Id;
            var minorLeafId = doubleNode.MinorLeaf.Id;
            return new SimpleDoubleNode(mainLeafId.ToString(), minorLeafId.ToString(), doubleNode.ConnectionKind, 0);
        }

        public SimpleDoubleNode ParseLines(List<string> lines)
        {
            Contract.Requires(lines != null);
            Contract.Requires(lines.Any());
            Contract.Ensures(Contract.Result<SimpleDoubleNode>() != null);

            var nodes = GetSimpleDoubleNodes(lines);

            var maxDepth = nodes.Max(node => node.Depth);
            int greatestCommonDivisor = 1;

            for (int i = maxDepth; i > 1; i--)
            {
                if (nodes.All(node => node.Depth % i == 0))
                {
                    greatestCommonDivisor = i;
                    break;
                }
            }
            nodes.ForEach(node => node.Depth /= greatestCommonDivisor);

            var result = nodes.First();

            for (var i = 1; i < nodes.Count(); i++)
            {
                var nearestParent = GetNearestParent(i, nodes);
                nearestParent.Add(nodes[i]);
            }

            return result;
        }

        private static List<SimpleDoubleNode> GetSimpleDoubleNodes(List<string> lines)
        {
            var rootNodes = lines.First().Split(new char[] {' '});
            if (rootNodes[0] != "Root" || rootNodes[2] != "Root")
            {
                throw new FileLoadException();
            }

            var root = new SimpleDoubleNode(rootNodes[0], rootNodes[2], ConnectionKind.Strict, 0);

            var nodes = new List<SimpleDoubleNode> {root};

            foreach (var line in lines)
            {
                var splittedLine = line.Split(new char[] {' '}, StringSplitOptions.None);
                if (splittedLine.Length != 2 || splittedLine.Length != 3)
                {
                    throw new FileLoadException();
                }

                var depth = line.TakeWhile(symbol => symbol == TabSeparator).Count();
                if (splittedLine.Length == 2)
                {
                    var mainLeafId = splittedLine[0];
                    var minorLeafId = splittedLine[1];
                    nodes.Add(new SimpleDoubleNode(mainLeafId, minorLeafId, ConnectionKind.None, depth));
                }
                else
                {
                    var mainLeafId = splittedLine[0];
                    var connectionSign = splittedLine[1];
                    var minorLeafId = splittedLine[2];
                    var connectionKind = connectionSign == "+"
                        ? ConnectionKind.Strict
                        : ConnectionKind.Relative;

                    nodes.Add(new SimpleDoubleNode(mainLeafId, minorLeafId, connectionKind, depth));
                }
            }
            return nodes;
        }

        private SimpleDoubleNode GetNearestParent(int index, List<SimpleDoubleNode> simpleDoubleNodes)
        {
            Contract.Requires(simpleDoubleNodes != null);
            Contract.Requires(simpleDoubleNodes.Any());
            Contract.Ensures(Contract.Result<SingleNode<StringId>>() != null);

            for (var i = index; i >= 0; i--)
            {
                if (simpleDoubleNodes[index].Depth - simpleDoubleNodes[i].Depth == 1)
                {
                    return simpleDoubleNodes[i];
                }
            }

            throw new InvalidOperationException("There is not parent");
        }

        private Queue<T> GetQueue<T>(T item)
        {
            return new Queue<T>(new[] { item });
        }
    }
}