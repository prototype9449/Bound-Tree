using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using BoundTree.Helpers.Actions;
using BoundTree.Logic;
using BoundTree.Logic.Trees;
using BoundTree.TreeReconstruction;
using Build.TestFramework;

namespace BoundTree.Helpers
{
    public class MultiTreeParser
    {
        private const string AddLongName = "add";
        private const string RemoveAllLongName = "remove all";
        private const string RemoveLongName = "remove";

        private readonly SingleTreeParser _singleTreeParser;
        private readonly NodeInfoFactory _nodeInfoFactory;
        private readonly TreeConstructor<StringId> _treeConstructor;

        public MultiTreeParser(TreeConstructor<StringId> treeConstructor, SingleTreeParser singleTreeParser, NodeInfoFactory nodeInfoFactory)
        {
            _treeConstructor = treeConstructor;
            _singleTreeParser = singleTreeParser;
            _nodeInfoFactory = nodeInfoFactory;
        }

        public MultiTree<StringId> GetMultiTree(List<string> lines)
        {
            Contract.Requires(lines != null);
            Contract.Requires(lines.Any());
            Contract.Ensures(Contract.Result<MultiTree<StringId>>() != null);

            var allBlocks = GetAllBlocks(lines);
            if (!allBlocks.Any())
            {
                throw new FileLoadException("Count of blocks is 0");
            }

            return GetMultiTree(allBlocks).First;
        }

        private Cortege<MultiTree<StringId>, LogHistory> GetMultiTree(List<List<string>> blocks)
        {
            Contract.Requires(blocks != null);
            Contract.Requires(blocks.Any());
            Contract.Requires(blocks.Count % 2 == 1);

            var logHistory = new LogHistory();

            var singleTree = _singleTreeParser.GetSingleTree(blocks[0]);
            logHistory.Add(new SingleTreeResult(singleTree));
            var mainTree = new MultiTree<StringId>(singleTree, _nodeInfoFactory);

            for (int i = 1; i < blocks.Count; i += 2)
            {
                var minorTree = _singleTreeParser.GetSingleTree(blocks[i]);
                var connectionBlock = blocks[i + 1];

                logHistory.Add(new SingleTreeResult(minorTree));
                logHistory.Add(new CommandsResult(connectionBlock));

                var bindController = new BindContoller<StringId>(mainTree, minorTree);
                AddConnections(bindController, connectionBlock);

                var idGenerator = new IdGenerator(mainTree.ToList());
                var multiNode = _treeConstructor.GetFilledTree(bindController, idGenerator).ToMultiNode();
                mainTree = new MultiTree<StringId>(multiNode, _nodeInfoFactory);
            }

            return new Cortege<MultiTree<StringId>, LogHistory>(mainTree, logHistory);
        }

        public Cortege<BindContoller<StringId>, LogHistory> GetBindContollerAndLogHistory(List<string> lines)
        {
            Contract.Requires(lines != null);
            Contract.Requires(lines.Any());

            var allBlocks = GetAllBlocks(lines);
            if (allBlocks.Count < 3)
            {
                throw new FileLoadException("Count of blocks is less then 3");
            }

            var allBlocksForMultiTree = allBlocks.Take(allBlocks.Count - 2).ToList();
            var linesForSingleTree = allBlocks[allBlocks.Count - 2];
            var commandLines = allBlocks.Last();

            var multiTreeAndLogHistory = GetMultiTree(allBlocksForMultiTree);
            var singleTree = _singleTreeParser.GetSingleTree(linesForSingleTree);

            var multiTree = multiTreeAndLogHistory.First;
            var logHistory = multiTreeAndLogHistory.Second;
            logHistory.Add(new SingleTreeResult(singleTree));
            logHistory.Add(new CommandsResult(commandLines));

            var bindController = new BindContoller<StringId>(multiTree, singleTree);
            AddConnections(bindController, commandLines);

            return new Cortege<BindContoller<StringId>, LogHistory>(bindController, logHistory);
        }

        private List<List<string>> GetAllBlocks(List<string> lines)
        {
            Contract.Requires(lines != null);
            Contract.Requires(lines.Any());

            List<List<string>> allBlocks = new List<List<string>>();

            var currentBlock = new List<string>();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line) && currentBlock.Any())
                {
                    allBlocks.Add(currentBlock.ToList());
                    currentBlock.Clear();
                }

                if (!string.IsNullOrEmpty(line))
                {
                    currentBlock.Add(line);
                }
            }

            return allBlocks;
        }

        private void AddConnections(BindContoller<StringId> bindContoller, List<string> commands)
        {
            Contract.Requires(bindContoller != null);
            Contract.Requires(commands != null);

            foreach (var command in commands)
            {
                var partsOfCommand = command.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (partsOfCommand[0] == AddLongName)
                {
                    bindContoller.Bind(new StringId(partsOfCommand[1]), new StringId(partsOfCommand[2]));
                }
                if (partsOfCommand[0] == RemoveAllLongName)
                {
                    bindContoller.RemoveAllConnections();
                }
                if (partsOfCommand[0] == RemoveLongName)
                {
                    bindContoller.RemoveConnection(new StringId(partsOfCommand[1]));
                }
            }
        }
    }
}