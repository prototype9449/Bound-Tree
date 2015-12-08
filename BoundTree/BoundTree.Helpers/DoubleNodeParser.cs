﻿using System;
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
            Contract.Ensures(Contract.Result<TreesData>() != null);

            var firstSpaceIndex = lines.FindIndex(line => line.Trim() == "");
            var minorTreeIndex = lines.Skip(firstSpaceIndex).ToList().FindIndex(line => line.Trim() != "") + firstSpaceIndex;

            if (minorTreeIndex > lines.Count)
            {
                throw new FileLoadException("Minor tree not found");
            }

            var secondSpaceIndex = lines.Skip(minorTreeIndex).ToList().FindIndex(line => line.Trim() == "") + minorTreeIndex;
            var connectionIndex = lines.Skip(secondSpaceIndex).ToList().FindIndex(line => line.Trim() != "") + secondSpaceIndex;
            if (connectionIndex > lines.Count)
            {
                throw new FileLoadException("Connection separator was not found");
            }

            var mainTreeLines = lines.Take(firstSpaceIndex).ToList();
            var minorTreeLines = lines
                .Skip(minorTreeIndex)
                .Take(secondSpaceIndex - minorTreeIndex).ToList();

            var connectionCommands = lines.Skip(connectionIndex).Where(line => line != "").ToList();
            
            var mainTree = _singleTreeParser.GetMultiTree(mainTreeLines);
            var minorTree = _singleTreeParser.GetSingleTree(minorTreeLines);

            var bindController = new BindContoller<StringId>(mainTree, minorTree);
            AddConnections(bindController, connectionCommands);
            var doubleNode = new TreeReconstruction<StringId>(bindController).GetFilledTree();

            //return new TreesData(doubleNode, mainTree, minorTree);

            throw new NotImplementedException();
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