using System;
using System.IO;
using BoundTree.Logic;

namespace BoundTree.Helpers.ConsoleHelper
{
    public class TreeLogger
    {
        public const string GeneralSeparator = "GeneralSeparator";
        public const string TreeSeparator = "TreeSeparator";

        private SingleTree<StringId> _mainTree;
        private SingleTree<StringId> _minorTree;
        private readonly string _pathToFile;

        private static readonly string FileName = "log.txt";
        
        public TreeLogger(SingleTree<StringId> mainTree, SingleTree<StringId> minorTree)
        {
            _mainTree = mainTree;
            _minorTree = minorTree;
            _pathToFile = Path.Combine(Directory.GetCurrentDirectory(), FileName);
            AddTreesToLogFile();
        }

        public TreeLogger(SingleTree<StringId> mainTree, SingleTree<StringId> minorTree, string pathToFile)
            : this(mainTree, minorTree)
        {
            _pathToFile = pathToFile;
        }

        public void ProcessCommand(string command)
        {
            File.AppendAllLines(_pathToFile, new[] { command });
        }

        public static DoubleNode<StringId> GetDoubleNodeFromFile(string pathToFile)
        {
            return new TreeFromLogBuilder().GetDoubleNodeFromFile(pathToFile);
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