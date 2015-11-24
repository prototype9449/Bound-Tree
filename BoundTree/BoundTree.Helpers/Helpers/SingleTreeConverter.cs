using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using BoundTree.Logic;
using BoundTree.Logic.Nodes;

namespace BoundTree.Helpers.Helpers
{
    public class SingleTreeConverter<T> where T : class, IEquatable<T>, new()
    {
        public List<string> ConvertTrees(SingleTree<T> mainSingleTree, SingleTree<T> minorSingleTree)
        {
            Contract.Requires(mainSingleTree != null);
            Contract.Requires(minorSingleTree != null);
            const int spaceBetweenTrees = 10;
            const char signBetweenTrees = ' ';

            var firstTreeLines = ConvertTree(mainSingleTree);
            var secondTreeLines = ConvertTree(minorSingleTree);

            var lines = new List<string>();

            var maxlength = Math.Max(firstTreeLines.Count(), secondTreeLines.Count());

            for (int i = 0; i < maxlength; i++)
            {
                var firstPart = i < firstTreeLines.Count
                    ? firstTreeLines[i]
                    : new string(signBetweenTrees, firstTreeLines.First().Length);

                var secondPart = i < secondTreeLines.Count
                    ? secondTreeLines[i]
                    : "";

                lines.Add(firstPart + new String(signBetweenTrees, spaceBetweenTrees) + secondPart);
                lines.Add(Environment.NewLine);
            }
            return lines;
        }

        public SingleTree<StringId> GetSingleTree(List<string> lines)
        {
            Contract.Requires(lines != null);
            Contract.Ensures(Contract.Result<SingleTree<StringId>>() != null);

            NodeInfo root = new Root();
            var nodes = GetList(new { NodeType = root, id = new StringId("Root"), Depth = 0 });

            foreach (var line in lines.Skip(1))
            {
                var splittedLine = line.Split(new[] { ' ', ')', '(' }, StringSplitOptions.RemoveEmptyEntries);
                if (!NodeInfoFactory.Contains(splittedLine[0]))
                {
                    throw new FileLoadException();
                }

                var nodeInfo = NodeInfoFactory.GetNodeInfo(splittedLine[0]);
                var id = new StringId(splittedLine[1]);
                var depth = line.TakeWhile(symbol => symbol == ' ').Count();
                nodes.Add(new { NodeType = nodeInfo, id = id, Depth = depth });
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

            var derivedNodes = nodes.Select(node => new SingleNode<StringId>(node.id, node.NodeType, node.Depth / greatestCommonDivisor)).ToList();

            var singleTree = new SingleTree<StringId>(derivedNodes.First());

            for (var i = 1; i < derivedNodes.Count(); i++)
            {
                var nearestParent = GetNearestParent(i, derivedNodes);
                nearestParent.Add(derivedNodes[i]);
            }

            return singleTree;
        }

        public List<string> ConvertTree(SingleTree<T> singleTree)
        {
            Contract.Requires(singleTree != null);
            Contract.Ensures(Contract.Result<List<string>>() != null);
            const int indent = 3;
            const char signBetweenTrees = ' ';

            if (singleTree == null)
                throw new ArgumentNullException("singleTree");

            var lines = new List<string>();

            if (singleTree.Root == null)
                return lines;

            var stack = new Stack<SingleNode<T>>(new[] { singleTree.Root });

            while (stack.Any())
            {
                var topElement = stack.Pop();

                var nodes = topElement.Nodes.ToList();
                nodes.Reverse();
                nodes.ForEach(node => stack.Push(node));

                var line = string.Format("{0}{1} ({2})", new string(signBetweenTrees, topElement.Node.Depth * indent),
                    topElement.Node.NodeInfo.GetType().Name, topElement.Node.Id);
                lines.Add(line);
            }

            var maxLength = lines.Max(line => line.Length);
            return lines.Select(line => line += new string(signBetweenTrees, maxLength - line.Length)).ToList();
        }

        private SingleNode<StringId> GetNearestParent(int index, List<SingleNode<StringId>> singleNodes)
        {
            Contract.Requires(singleNodes != null);
            Contract.Ensures(Contract.Result<SingleNode<StringId>>() != null);

            for (var i = index; i >= 0; i--)
            {
                if (singleNodes[index].Node.Depth - singleNodes[i].Node.Depth == 1)
                {
                    return singleNodes[i];
                }
            }

            throw new InvalidOperationException("There is not parent");
        }

        private List<T> GetList<T>(T item)
        {
            return new List<T>(new[] { item });
        }
    }
}