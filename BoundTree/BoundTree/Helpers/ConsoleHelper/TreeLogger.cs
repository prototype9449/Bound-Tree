using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BoundTree.Logic;

namespace BoundTree.Helpers.ConsoleHelper
{
    public class TreeLogger
    {
        private SingleTree<StringId> _mainTree;
        private SingleTree<StringId> _minorTree;
        private static readonly string FileName = "log.txt";
        private const string GeneralSeparator = "GeneralSeparator";
        private const string TreeSeparator = "TreeSeparator";

        private static string _pathToFile;

        static TreeLogger()
        {
            _pathToFile = Path.Combine(Directory.GetCurrentDirectory(), FileName);
        }

        public TreeLogger(SingleTree<StringId> mainTree, SingleTree<StringId> minorTree)
        {
            _mainTree = mainTree;
            _minorTree = minorTree;
            AddTreesToLogFile();
        }

        public void ProcessCommand(string command)
        {
            File.AppendAllLines(_pathToFile, new[] { command });
        }

        private void AddTreesToLogFile()
        {
            var mainTreeLines = new ConsoleTreeWriter<StringId>().GetNodeLines(_mainTree);
            var minorTreeLines = new ConsoleTreeWriter<StringId>().GetNodeLines(_minorTree);
            mainTreeLines.Add(Environment.NewLine);
            mainTreeLines.Add(TreeSeparator);
            mainTreeLines.Add(Environment.NewLine);
            mainTreeLines.AddRange(minorTreeLines);
            mainTreeLines.Add(Environment.NewLine);

            File.Create(_pathToFile).Close();
            File.AppendAllLines(_pathToFile, mainTreeLines);
            File.AppendAllLines(_pathToFile, new[] { GeneralSeparator });
        }

        public static DoubleNode<StringId> GetDoubleNodeFromFile()
        {
            if (!File.Exists(_pathToFile))
            {
                throw new FileNotFoundException(_pathToFile);
            }

            var lines = File.ReadAllLines(_pathToFile).ToList();
            lines.RemoveAll(line => line == "");

            var minorTreeIndex = lines.FindIndex(line => line.Contains(TreeSeparator)) + 1;
            if (minorTreeIndex > lines.Count)
            {
                throw new FileLoadException("Minor tree not found");
            }
            var connectionIndex = lines.FindIndex(line => line.Contains(GeneralSeparator)) + 1;
            if (connectionIndex > lines.Count)
            {
                throw new FileLoadException("Connection separator was not found");
            }

            var mainTreeLines = lines.Take(minorTreeIndex - 1).ToList();
            var minorTreeLines = lines
                .Skip(mainTreeLines.Count + 1)
                .Take(connectionIndex - minorTreeIndex - 1).ToList();
            var connectionLines = lines.Skip(connectionIndex).ToList();

            return null;
        }

        private SingleTree<StringId> GetSingleTree(List<string> treeLines)
        {
            SingleNode<StringId> singleNodes;

            var depths = treeLines.Select(line => line.TakeWhile(symbol => symbol == ' ').Count());
            var maxDeep = depths.Max();
            int GCD = 1;

            for (int i = 2; i < maxDeep / 2; i++)
            {
                if (depths.All(deep => deep % i == 0))
                {
                    GCD = i;
                }
            }

            var devidedDepths = depths.Select(deep => deep % GCD);
            var nodeInfoTypes = treeLines.Select(line => NodeInfoFactory.GetNodeInfo(line.Split(new[] { ' ' })[0]));
            foreach (var line in treeLines)
            {
                var splittedLine = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (!NodeInfoFactory.Contains(splittedLine[0]))
                {
                    throw new FileLoadException();
                }

                var nodeInfo = NodeInfoFactory.GetNodeInfo(splittedLine[0]);
                //var id = todo by means of regex
            }

            return null;
        }
    }
}