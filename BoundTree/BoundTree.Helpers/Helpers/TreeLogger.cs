using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using BoundTree.Logic;

namespace BoundTree.Helpers.Helpers
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

        public void AddDoubleTreeToFile(DoubleNode<StringId> currentDoubleNode)
        {
            Contract.Requires(currentDoubleNode != null);

            var lines = new DoubleNodeConverter().ConvertDoubleNode(currentDoubleNode);
            lines.Insert(0, Environment.NewLine);
            File.AppendAllLines(_pathToFile, lines);
        }

        private static string GetStandartFilePath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), FileName);
        }

        private void AddTreesToLogFile()
        {
            var singleTreeConverter = new SingleTreeConverter<StringId>();
            var mainTreeLines = singleTreeConverter.ConvertTree(_mainTree);
            var minorTreeLines = singleTreeConverter.ConvertTree(_minorTree);

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