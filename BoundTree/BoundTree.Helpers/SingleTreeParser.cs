using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using BoundTree.Logic;
using BoundTree.Logic.Nodes;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;

namespace BoundTree.Helpers
{
    public class SingleTreeParser
    {
        private const char SpaceSeparator = ' ';
        private const char TabSeparator = '\t';


        public MultiTree<StringId> GetMultiTree(List<string> lines)
        {
            Contract.Requires(lines != null);
            Contract.Requires(lines.Any());
            Contract.Ensures(Contract.Result<MultiTree<StringId>>() != null);

//            var indention = lines.Any(line => line.Contains(TabSeparator)) ? TabSeparator : SpaceSeparator;
//
//            NodeInfo root = new Root();
//            var nodes = GetList(new { NodeType = root, Id = new StringId("Root"), Depth = 0 });
//
//            foreach (var line in lines.Skip(1))
//            {
//                var splittedLine = line.Split(new[] { SpaceSeparator, TabSeparator, ')', '(' }, StringSplitOptions.RemoveEmptyEntries);
//                if (!NodeInfoFactory.Contains(splittedLine[0]))
//                {
//                    throw new FileLoadException();
//                }
//
//                var nodeInfo = NodeInfoFactory.GetNodeInfo(splittedLine[0]);
//                var id = new StringId(splittedLine[1]);
//                var depth = line.TakeWhile(symbol => symbol == indention).Count();
//                nodes.Add(new { NodeType = nodeInfo, Id = id, Depth = depth });
//            }
//
//            var greatestCommonDivisor = GetGreatestCommonDivisor(nodes);
//
//            var derivedNodes = nodes.Select(node => new SingleNode<StringId>(node.Id, node.NodeType, node.Depth / greatestCommonDivisor)).ToList();
//
//            var singleTree = new SingleTree<StringId>(derivedNodes.First());
//
//            for (var i = 1; i < derivedNodes.Count(); i++)
//            {
//                var nearestParent = GetNearestParent(i, derivedNodes);
//                nearestParent.Add(derivedNodes[i]);
//            }
//
//            return singleTree;

            throw new NotImplementedException();
        }

        public SingleTree<StringId> GetSingleTree(List<string> lines)
        {
            Contract.Requires(lines != null);
            Contract.Requires(lines.Any());
            Contract.Ensures(Contract.Result<SingleTree<StringId>>() != null);

            var indention = lines.Any(line => line.Contains(TabSeparator)) ? TabSeparator : SpaceSeparator;

            NodeInfo root = new Root();
            var nodes = GetList(new { NodeType = root, Id = new StringId("Root"), Depth = 0 });

            foreach (var line in lines.Skip(1))
            {
                var splittedLine = line.Split(new[] { SpaceSeparator, TabSeparator,')', '(' }, StringSplitOptions.RemoveEmptyEntries);
                if (!NodeInfoFactory.Contains(splittedLine[0]))
                {
                    throw new FileLoadException();
                }

                var nodeInfo = NodeInfoFactory.GetNodeInfo(splittedLine[0]);
                var id = new StringId(splittedLine[1]);
                var depth = line.TakeWhile(symbol => symbol == indention).Count();
                nodes.Add(new { NodeType = nodeInfo, Id = id, Depth = depth });
            }

            var greatestCommonDivisor = GetGreatestCommonDivisor(nodes);

            var derivedNodes = nodes.Select(node => new SingleNode<StringId>(node.Id, node.NodeType, node.Depth / greatestCommonDivisor)).ToList();

            var singleTree = new SingleTree<StringId>(derivedNodes.First());

            for (var i = 1; i < derivedNodes.Count(); i++)
            {
                var nearestParent = GetNearestParent(i, derivedNodes);
                nearestParent.Add(derivedNodes[i]);
            }

            return singleTree;
        }

        private static int GetGreatestCommonDivisor(IEnumerable<dynamic> nodes)
        {
            var maxDepth = nodes.Max(node => node.Depth);
            int greatestCommonDivisor = 1;

            for (int i = maxDepth; i > 1; i--)
            {
                if (nodes.All(node => node.Depth%i == 0))
                {
                    greatestCommonDivisor = i;
                    break;
                }
            }
            return greatestCommonDivisor;
        }

        private U GetNearestParent<U>(int index, List<U> singleNodes) where U : INode<StringId>
        {
            Contract.Requires(singleNodes != null);
            Contract.Ensures(Contract.Result<SingleNode<StringId>>() != null);

            for (var i = index; i >= 0; i--)
            {
                if (singleNodes[index].Depth - singleNodes[i].Depth == 1)
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