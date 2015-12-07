using System;
using System.Diagnostics.Contracts;
using System.IO;
using BoundTree.Logic;
using BoundTree.Logic.Trees;

namespace BoundTree.Helpers
{
    public class TreeLogger
    {
        private static TreeLogger _instance = new TreeLogger();

        private readonly TreeConverter<StringId> _treeConverter = new TreeConverter<StringId>();
        private readonly string _pathToFile;
        private const string DefaultFileName = " log.txt";

        private TreeLogger()
        {
            _pathToFile = GetStandartFilePath();
        }

        public static TreeLogger GetTreeLogger()
        {
            return _instance;
        }

        public void ProcessCommand(string command)
        {
            Contract.Requires(!string.IsNullOrEmpty(command));

            File.AppendAllLines(_pathToFile, new[] { command });
        }

        private static string GetStandartFilePath()
        {
            var prefixFileName = DateTime.Now.ToString("yyyy.MM.dd H_mm_ss");
            var directoryName = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

            var fileName = Path.Combine(directoryName, prefixFileName + DefaultFileName);

            return Path.Combine(directoryName, fileName);
        }

        public void AddSinlgeTreeInFile(SingleTree<StringId> singleTree)
        {
            var lines = _treeConverter.ConvertSingleTree(singleTree);
            lines.Insert(0, Environment.NewLine);
            lines.Add(Environment.NewLine);
            File.AppendAllLines(_pathToFile, lines);
        }

        public void AddMultiTreeInFile(MultiTree<StringId> multiTree)
        {
            var lines = _treeConverter.ConvertMultiTree(multiTree);
            lines.Insert(0, Environment.NewLine);
            lines.Add(Environment.NewLine);
            File.AppendAllLines(_pathToFile, lines);
        }
    }

}