using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using BoundTree.Logic;

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
            return new DoubleNodeConverter().GetDoubleNode(lines);
        }
    }
}