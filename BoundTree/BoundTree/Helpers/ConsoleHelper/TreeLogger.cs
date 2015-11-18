using System;
using System.IO;
using System.Linq;
using BoundTree.Logic;

namespace BoundTree.Helpers.ConsoleHelper
{
    public class TreeLogger
    {
        private SingleTree<StringId> _mainTree;
        private SingleTree<StringId> _minorTree;
        private const string FileName = "log.txt";
        private string _pathToFile;

        public TreeLogger(SingleTree<StringId> mainTree, SingleTree<StringId> minorTree)
        {
            _pathToFile = Path.Combine(Directory.GetCurrentDirectory(), FileName);
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
            mainTreeLines.AddRange(minorTreeLines);
            mainTreeLines.Add(Environment.NewLine);

            File.Create(_pathToFile).Close();
            File.AppendAllLines(_pathToFile, mainTreeLines);
        }
    }
}