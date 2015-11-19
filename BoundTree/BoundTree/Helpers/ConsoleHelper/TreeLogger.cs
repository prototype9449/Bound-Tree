using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BoundTree.Logic;
using BoundTree.Logic.Nodes;

namespace BoundTree.Helpers.ConsoleHelper
{
    public class TreeLogger
    {
        private SingleTree<StringId> _mainTree;
        private SingleTree<StringId> _minorTree;
        private static readonly string FileName = "log.txt";
        public const string GeneralSeparator = "GeneralSeparator";
        public const string TreeSeparator = "TreeSeparator";

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

        public static DoubleNode<StringId> GetDoubleNode()
        {
            return new TreeFromLogBuilder().GetDoubleNodeFromFile(_pathToFile);
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
    }
}