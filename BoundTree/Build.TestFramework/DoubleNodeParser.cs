using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using BoundTree;
using BoundTree.Logic;
using BoundTree.TreeReconstruction;

namespace Build.TestFramework
{
    public class DoubleNodeParser
    {
        private const int SpaceCount = 2;
        private const string AddLongName = "add";
        private const string RemoveAllLongName = "remove all";
        private const string RemoveLongName = "remove";

        public DoubleNode<StringId> GetDoubleNode(List<string> lines)
        {
            Contract.Ensures(Contract.Result<DoubleNode<StringId>>() != null);

            var minorTreeIndex = lines.FindIndex(line => line == "") + SpaceCount;

            if (minorTreeIndex > lines.Count)
            {
                throw new FileLoadException("Minor tree not found");
            }

            var connectionIndex = lines.Skip(minorTreeIndex).ToList().FindIndex(line => line == "") + SpaceCount + minorTreeIndex;
            if (connectionIndex > lines.Count)
            {
                throw new FileLoadException("Connection separator was not found");
            }

            var mainTreeLines = lines.Take(minorTreeIndex - SpaceCount).ToList();
            var minorTreeLines = lines
                .Skip(minorTreeIndex)
                .Take(connectionIndex - minorTreeIndex - SpaceCount).ToList();

            var connectionCommands = lines.Skip(connectionIndex).ToList();

            var singleTreeConverter = new SingleTreeParser();

            var mainTree =singleTreeConverter.GetSingleTree(mainTreeLines);
            var minorTree = singleTreeConverter.GetSingleTree(minorTreeLines);

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