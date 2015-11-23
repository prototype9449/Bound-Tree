using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using BoundTree.Logic;

namespace BoundTree.Helpers.ConsoleHelper
{
    public class TreeLogger
    {
        private readonly SingleTree<StringId> _mainTree;
        private readonly SingleTree<StringId> _minorTree;
        private readonly string _pathToFile;

        private const string FileName = "log.txt";

        public TreeLogger(SingleTree<StringId> mainTree, SingleTree<StringId> minorTree)
        {
            Contract.Requires(mainTree != null);
            Contract.Requires(minorTree != null);

            _mainTree = mainTree;
            _minorTree = minorTree;
            _pathToFile = GetStandartFilePath();
            AddTreesToLogFile();
        }

        public TreeLogger(SingleTree<StringId> mainTree, SingleTree<StringId> minorTree, string pathToFile)
            : this(mainTree, minorTree)
        {
            Contract.Requires(!string.IsNullOrEmpty(pathToFile));

            _pathToFile = pathToFile;
        }

        public void ProcessCommand(string command)
        {
            Contract.Requires(!string.IsNullOrEmpty(command));

            File.AppendAllLines(_pathToFile, new[] { command });
        }

        public static DoubleNode<StringId> GetDoubleNodeFromFile(string pathToFile)
        {
            Contract.Requires(!string.IsNullOrEmpty(pathToFile));

            return new TreeFromLogBuilder().GetDoubleNodeFromFile(pathToFile);
        }

        public static DoubleNode<StringId> GetDoubleNodeFromFile()
        {
            var pathToFile = GetStandartFilePath();
            return new TreeFromLogBuilder().GetDoubleNodeFromFile(pathToFile);
        }

        private static string GetStandartFilePath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), FileName);
        }

        private void AddTreesToLogFile()
        {
            var mainTreeLines = new SingleTreeConverter<StringId>().ConvertTree(_mainTree);
            var minorTreeLines = new SingleTreeConverter<StringId>().ConvertTree(_minorTree);

            var result = new List<string>();
            result.AddRange(mainTreeLines);
            result.Add(Environment.NewLine);
            result.AddRange(minorTreeLines);
            result.Add(Environment.NewLine);

            File.Create(_pathToFile).Close();
            File.AppendAllLines(_pathToFile, result);
        }
    }
}