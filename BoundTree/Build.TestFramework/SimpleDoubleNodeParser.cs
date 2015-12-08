using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BoundTree.Logic;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;

namespace Build.TestFramework
{
    public class SimpleDoubleNodeParser
    {
        private const char TabIndention = '\t';
        private const char SpaceIndention = ' ';
        private const string SignStrictConnection = "+";
        private const string SignRelativeConnection = "*";
        private const string EmptyNodeName = "()";
        private const string EmptyLine = "";

        public SimpleMultiNode ParseToSimpleMultiTree(MultiTree<StringId> multiTree)
        {
            Contract.Requires(multiTree != null);
            Contract.Ensures(Contract.Result<SimpleMultiNode>() != null);

            var result = GetSimpleDoubleNode(multiTree.Root);

            var root = new { doubleNode = multiTree, simpleDoubleNode = result };
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

        private SimpleMultiNode GetSimpleDoubleNode(MultiNode<StringId> multiNode)
        {
            Contract.Requires(multiNode != null);
            Contract.Ensures(Contract.Result<SimpleMultiNode>() != null);

            var mainLeafId = multiNode.Id.ToString();
            var minorLeafIds = multiNode.MultiNodeData.MinorDataNodes.Select(node => new SimpleNodeData(node.ConnectionKind, node.NodeData.Id.ToString()));

            return new SimpleMultiNode(mainLeafId, minorLeafIds, multiNode., 0);
        }

        public SimpleMultiNode ParseLines(List<string> lines)
        {
            Contract.Requires(lines != null);
            Contract.Requires(lines.Any());
            Contract.Ensures(Contract.Result<SimpleMultiNode>() != null);

            var nodes = GetSimpleDoubleNodes(lines);

            foreach (var simpleDoubleNode in nodes)
            {
                if (simpleDoubleNode.MinorLeafId == EmptyNodeName)
                {
                    simpleDoubleNode.MinorLeafId = EmptyLine;
                }
                if (simpleDoubleNode.MainLeafId == EmptyNodeName)
                {
                    simpleDoubleNode.MainLeafId = EmptyLine;
                }
            }

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

        private List<SimpleMultiNode> GetSimpleDoubleNodes(List<string> lines)
        {
            Contract.Requires(lines != null);
            Contract.Requires(lines.Any());
            Contract.Ensures(Contract.Result<List<SimpleMultiNode>>() != null);
            Contract.Ensures(Contract.Result<List<SimpleMultiNode>>().Any());

            var indention = lines.Any(line => line.Contains(TabIndention)) ? TabIndention : SpaceIndention;

            var rootNodes = lines.First().Split(new[] { SpaceIndention, TabIndention }, StringSplitOptions.RemoveEmptyEntries);

            if (rootNodes[0] != "Root" || rootNodes[2] != "Root")
            {
                throw new FileLoadException();
            }

            var root = new SimpleMultiNode(rootNodes[0], rootNodes[2], ConnectionKind.Strict, 0);

            var nodes = new List<SimpleMultiNode> { root };

            foreach (var line in lines.Skip(1))
            {
                var splittedLine = line.Split(new [] {SpaceIndention, TabIndention }, StringSplitOptions.RemoveEmptyEntries);
                if (splittedLine.Length != 2 && splittedLine.Length != 3)
                {
                    throw new FileLoadException();
                }

                var depth = line.TakeWhile(symbol => symbol == indention).Count();
                if (splittedLine.Length == 2)
                {
                    var mainLeafId = splittedLine[0];
                    var minorLeafId = splittedLine[1];
                    nodes.Add(new SimpleMultiNode(mainLeafId, minorLeafId, ConnectionKind.None, depth));
                }
                else
                {
                    var mainLeafId = splittedLine[0];
                    var connectionSign = splittedLine[1];
                    var minorLeafId = splittedLine[2];
                    var connectionKind = GetConnectionKind(connectionSign);

                    nodes.Add(new SimpleMultiNode(mainLeafId, minorLeafId, connectionKind, depth));
                }
            }

            return nodes;
        }

        private ConnectionKind GetConnectionKind(string sign)
        {
            switch (sign)
            {
                case SignStrictConnection:
                    return ConnectionKind.Strict;
                case SignRelativeConnection:
                    return ConnectionKind.Relative;
            }

            return ConnectionKind.None;
        }

        private SimpleMultiNode GetNearestParent(int index, List<SimpleMultiNode> simpleDoubleNodes)
        {
            Contract.Requires(simpleDoubleNodes != null);
            Contract.Requires(simpleDoubleNodes.Any());
            Contract.Ensures(Contract.Result<SimpleMultiNode>() != null);

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