using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using BoundTree.Helpers.TreeReconstruction;
using BoundTree.Logic;
using BoundTree.Logic.Nodes;

namespace BoundTree.Helpers.ConsoleHelper
{
    public class TreeFromLogBuilder
    {
        public DoubleNode<StringId> GetDoubleNodeFromFile(string pathToFile)
        {
            Contract.Requires(!string.IsNullOrEmpty(pathToFile));
            Contract.Requires<FileNotFoundException>(File.Exists(pathToFile));
            Contract.Ensures(Contract.Result<DoubleNode<StringId>>() != null);

            var lines = File.ReadAllLines(pathToFile).ToList();

            var minorTreeIndex = lines.FindIndex(line => line == "") + 2;

            if (minorTreeIndex > lines.Count)
            {
                throw new FileLoadException("Minor tree not found");
            }

            var connectionIndex = lines.Skip(minorTreeIndex + 1).ToList().FindIndex(line => line == "") + 3 + minorTreeIndex;
            if (connectionIndex > lines.Count)
            {
                throw new FileLoadException("Connection separator was not found");
            }

            var mainTreeLines = lines.Take(minorTreeIndex - 1).ToList();
            var minorTreeLines = lines
                .Skip(mainTreeLines.Count + 1)
                .Take(connectionIndex - minorTreeIndex - 1).ToList();

            var connectionCommands = lines.Skip(connectionIndex).ToList();

            var mainTree = GetSingleTree(mainTreeLines);
            var minorTree = GetSingleTree(minorTreeLines);

            var bindController = new BindContoller<StringId>(mainTree, minorTree);
            AddConnections(bindController, connectionCommands);
            return new TreeReconstruction<StringId>(bindController).GetFilledTree();
        }

        private void AddConnections(BindContoller<StringId> bindContoller, List<string> commands)
        {
            Contract.Requires(bindContoller != null);
            Contract.Requires(commands != null);

            foreach (var command in commands)
            {
                var partsOfCommand = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (partsOfCommand[0] == CommandMediator.AddLongName)
                {
                    bindContoller.Bind(new StringId(partsOfCommand[1]), new StringId(partsOfCommand[2]));
                }
                if (partsOfCommand[0] == CommandMediator.RemoveAllLongName)
                {
                    bindContoller.RemoveAllConnections();
                }
                if (partsOfCommand[0] == CommandMediator.RemoveLongName)
                {
                    bindContoller.RemoveConnection(new StringId(partsOfCommand[1]));
                }
            }
        }

        private SingleTree<StringId> GetSingleTree(List<string> treeLines)
        {
            Contract.Requires(treeLines != null);
            Contract.Ensures(Contract.Result<SingleTree<StringId>>() != null);

            NodeInfo root = new Root();
            var nodes = GetList(new { NodeType = root, id = new StringId("Root"), Depth = 0 });

            foreach (var line in treeLines.Skip(1))
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

        private SingleNode<StringId> GetNearestParent(int index, List<SingleNode<StringId>> singleNodes)
        {
            Contract.Requires(singleNodes != null);
            Contract.Ensures(Contract.Result<SingleNode<StringId>>() != null);

            for (var i = index; i >= 0; i--)
            {
                if (singleNodes[index].Node.Depth - singleNodes[i].Node.Depth== 1)
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