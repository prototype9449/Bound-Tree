using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using BoundTree.Helpers;
using BoundTree.Logic;
using BoundTree.Logic.Trees;

namespace Build.TestFramework
{
    public class ValidationController
    {
        private readonly SimpleMultiNodeParser _multiNodeParser = new SimpleMultiNodeParser();
        private readonly MultiTreeParser _multiTreeParser;

        public ValidationController(MultiTreeParser multiTreeParser)
        {
            _multiTreeParser = multiTreeParser;
        }

        public bool IsValid(string pathToFile)
        {
            Contract.Requires(!string.IsNullOrEmpty(pathToFile));
            Contract.Requires<FileNotFoundException>(File.Exists(pathToFile));

            var actual = _multiNodeParser.ParseToSimpleMultiNode(GetActualMultiTreeFromFile(pathToFile));
            var expected = _multiNodeParser.ParseToSimpleMultiNode(GetExpectedMultiTreeLines(pathToFile));

            return actual.Equals(expected);
        }

        private MultiTree<StringId> GetActualMultiTreeFromFile(string pathToFile)
        {
            Contract.Requires(!String.IsNullOrEmpty(pathToFile));
            Contract.Requires<FileNotFoundException>(File.Exists(pathToFile));
            Contract.Ensures(Contract.Result<MultiTree<StringId>>() != null);

            var lines = File.ReadAllLines(pathToFile).ToList();

            for (int i = lines.Count - 1; i >= 0; i--)
            {
                if (lines[i] == "")
                {
                    var resultLines = lines.Take(i + 1).ToList();
                    return _multiTreeParser.GetMultiTree(resultLines);
                }
            }

            throw new FileLoadException();
        }

        private List<string> GetExpectedMultiTreeLines(string pathToFile)
        {
            Contract.Requires(!String.IsNullOrEmpty(pathToFile));
            Contract.Requires<FileNotFoundException>(File.Exists(pathToFile));
            Contract.Ensures(Contract.Result<List<string>>() != null);

            var lines = File.ReadAllLines(pathToFile).ToList();
            var resultLines = new List<string>();

            for (int i = lines.Count() -1 ; i >= 0 ; i--)
            {
                if (lines[i] != "")
                {
                    resultLines.Insert(0, lines[i]);
                }
                else
                {
                    break;
                }
            }

            return resultLines;
        }
    }
}