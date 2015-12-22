using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BoundTree.Helpers;
using BoundTree.Logic;
using BoundTree.Logic.NodeData;
using BoundTree.Logic.Nodes;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;

namespace Build.TestFramework
{
    public class SimpleMultiNodeParser
    {
        private const char TabIndention = '\t';
        private const char SpaceIndention = ' ';
        private const string EmptyNodeName = "()";
        private const string EmptyLine = "";

        public SimpleMultiNode ParseToSimpleMultiNode(MultiTree<StringId> multiTree)
        {
            Contract.Requires(multiTree != null);
            Contract.Ensures(Contract.Result<SimpleMultiNode>() != null);

            var result = ParseToSimpleMultiNode(multiTree.Root);

            var root = new { multiNode = multiTree.Root, simpleDoubleNode = result };
            var queue = GetQueue(root);

            while (queue.Any())
            {
                var current = queue.Dequeue();
                foreach (var multiNode in current.multiNode.Childs)
                {
                    var simpleDoubleNode = ParseToSimpleMultiNode(multiNode);
                    queue.Enqueue(new { multiNode, simpleDoubleNode });
                    current.simpleDoubleNode.Add(simpleDoubleNode);
                }
            }

            return result;
        }

        private SimpleMultiNode ParseToSimpleMultiNode(MultiNode<StringId> multiNode)
        {
            Contract.Requires(multiNode != null);
            Contract.Ensures(Contract.Result<SimpleMultiNode>() != null);

            var mainLeafId = multiNode.Id.ToString();
            var minorLeafIds = multiNode.MultiNodeData.MinorDataNodes.Select(node => new SimpleNodeData(node.ConnectionKind, node.NodeData.ToString(), node.IsEmpty())).ToList();

            return new SimpleMultiNode(mainLeafId, 0, multiNode.IsEmpty(), minorLeafIds);
        }

        public SimpleMultiNode ParseToSimpleMultiNode(List<string> lines)
        {
            Contract.Requires(lines != null);
            Contract.Requires(lines.Any());
            Contract.Ensures(Contract.Result<SimpleMultiNode>() != null);

            var multiNodes = GetSimpleMultiNodes(lines);
            var result = multiNodes.First();

            var maxDepth = multiNodes.Max(node => node.Depth);
            int greatestCommonDivisor = 1;

            for (int i = maxDepth; i > 1; i--)
            {
                if (multiNodes.All(node => node.Depth % i == 0))
                {
                    greatestCommonDivisor = i;
                    break;
                }
            }
            multiNodes.ForEach(node => node.Depth /= greatestCommonDivisor);

            for (var i = 1; i < multiNodes.Count(); i++)
            {
                var nearestParent = GetNearestParent(i, multiNodes);
                nearestParent.Add(multiNodes[i]);
            }

            return result;
        }

        private List<SimpleMultiNode> GetSimpleMultiNodes(List<string> lines)
        {
            Contract.Requires(lines != null);
            Contract.Requires(lines.Any());
            Contract.Ensures(Contract.Result<List<SimpleMultiNode>>() != null);
            Contract.Ensures(Contract.Result<List<SimpleMultiNode>>().Any());

            var simpleMultiNodes = new List<SimpleMultiNode>();

            var indention = lines.Any(line => line.Contains(TabIndention)) ? TabIndention : SpaceIndention;

            foreach (var line in lines)
            {
                var lineParts = GetMainNodeAndMinorNodes(line);
                var idAndDepth = GetNodeData(lineParts.First, indention);
                var simpleDataNodes = GetSimpleDataNodes(lineParts.Second);
                simpleMultiNodes.Add(new SimpleMultiNode(idAndDepth.First, idAndDepth.Second, simpleDataNodes));
            }

            return simpleMultiNodes;
        }

        private Cortege<string, string> GetMainNodeAndMinorNodes(string line)
        {
            var stopSymbols = new HashSet<char>()
            {
                ConnectionSignHelper.NoneConnectionSign,
                ConnectionSignHelper.RelativeConnectionSign,
                ConnectionSignHelper.StrictConnectionSign
            };

            var mainNodePart = string.Join("",line.TakeWhile(symbol => !stopSymbols.Contains(symbol)));
            var minorNodesPart = string.Join("",line.SkipWhile(symbol => !stopSymbols.Contains(symbol)));

            return new Cortege<string, string>(mainNodePart, minorNodesPart);
        }

        private List<ConnectionKind> GetAllConnectionKinds(string line)
        {
            var allKinds = new List<ConnectionKind>();

            for (int i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case ConnectionSignHelper.NoneConnectionSign: allKinds.Add(ConnectionKind.None);
                        break;
                    case ConnectionSignHelper.RelativeConnectionSign: allKinds.Add(ConnectionKind.Relative);
                        break;
                    case ConnectionSignHelper.StrictConnectionSign: allKinds.Add(ConnectionKind.Strict);
                        break;
                }
            }

            return allKinds;
        }

        private List<SimpleNodeData> GetSimpleDataNodes(string line)
        {
            var simpleDataNodes = new List<SimpleNodeData>();

            var connectionKinds = GetAllConnectionKinds(line);
            var idNodes = line.Split(new[] {' ', ConnectionSignHelper.NoneConnectionSign, ConnectionSignHelper.RelativeConnectionSign,
                        ConnectionSignHelper.StrictConnectionSign}, StringSplitOptions.RemoveEmptyEntries);

            if (connectionKinds.Count() != idNodes.Length)
                throw new FileLoadException("count of nodes are not the same");

            for (int i = 0; i < idNodes.Length; i++)
            {
                if (idNodes[i].Contains('('))
                {
                    var id = idNodes[i].Trim('(', ')');
                    simpleDataNodes.Add(new SimpleNodeData(connectionKinds[i], id, true));
                }
                else
                {
                    simpleDataNodes.Add(new SimpleNodeData(connectionKinds[i], idNodes[i], false));
                }
            }

            return simpleDataNodes;
        }

        private Cortege<string, int> GetNodeData(string line, char indention)
        {
            var depth = line.TakeWhile(symbol => symbol == indention).Count();
            var typeAndid = line.Split(new[] { '(', ')', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var id = typeAndid.FirstOrDefault() ?? "";

            return new Cortege<string, int>(id, depth);
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