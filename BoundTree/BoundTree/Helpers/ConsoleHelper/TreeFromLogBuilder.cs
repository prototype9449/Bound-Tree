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
            //Contract.Requires<FileNotFoundException>(File.Exists(pathToFile));
            Contract.Ensures(Contract.Result<DoubleNode<StringId>>() != null);

            var lines = File.ReadAllLines(pathToFile).ToList();
            return new DoubleNodeConverter<StringId>().GetDoubleNode(lines);
        }
    }
}