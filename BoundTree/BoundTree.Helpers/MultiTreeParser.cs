using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using BoundTree.Logic;
using BoundTree.Logic.Trees;
using BoundTree.TreeReconstruction;

namespace BoundTree.Helpers
{
    public class MultiTreeParser
    {
        private const string AddLongName = "add";
        private const string RemoveAllLongName = "remove all";
        private const string RemoveLongName = "remove";

        private readonly SingleTreeParser _singleTreeParser = new SingleTreeParser();

        public MultiTree<StringId> GetMultiTree(List<string> lines)
        {
            Contract.Requires(lines != null);
            Contract.Requires(lines.Any());
            Contract.Ensures(Contract.Result<TreesData>() != null);

            var allBlocks = GetAllBlocks(lines);
            if (!allBlocks.Any())
            {
                throw new FileLoadException("Count of blocks is 0");
            }

            allBlocks.Remove(allBlocks.Last());

            var mainTree = new MultiTree<StringId>(_singleTreeParser.GetSingleTree(allBlocks[0]));
           

            for (int i = 3; i < allBlocks.Count; i+=2)
            {
                var minorTree = _singleTreeParser.GetSingleTree(allBlocks[i]);
                var bindController = new BindContoller<StringId>(mainTree, minorTree);
                AddConnections(bindController, allBlocks[i+1]);
                mainTree = new MultiTree<StringId>(new TreeReconstruction<StringId>(bindController).GetFilledTree().ToMultiNode());
            }

            return mainTree;
        }

        private List<List<string>> GetAllBlocks(List<string> lines)
        {
            List<List<string>> allBlocks = new List<List<string>>();


            var currentBlock = new List<string>();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line) && currentBlock.Any())
                {
                    allBlocks.Add(currentBlock);
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