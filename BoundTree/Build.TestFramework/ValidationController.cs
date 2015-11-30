using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using BoundTree.Logic;

namespace Build.TestFramework
{
    public class ValidationController
    {
        public bool IsValid(string pathToFile)
        {
            Contract.Requires(!string.IsNullOrEmpty(pathToFile));
            Contract.Requires<FileNotFoundException>(File.Exists(pathToFile));

            var actual = new SimpleDoubleNodeParser().ParseDoubleNode(GetDoubleNodeFromFile(pathToFile));
            var expected = new SimpleDoubleNodeParser().ParseLines(GetExpectedDoubleNodeLines(pathToFile));

            return actual.Equals(expected);
        }

        private DoubleNode<StringId> GetDoubleNodeFromFile(string pathToFile)
        {
            Contract.Requires(!String.IsNullOrEmpty(pathToFile));
            Contract.Requires<FileNotFoundException>(File.Exists(pathToFile));
            Contract.Ensures(Contract.Result<DoubleNode<StringId>>() != null);

            var lines = File.ReadAllLines(pathToFile).ToList();

            for (int i = lines.Count - 1; i >= 0; i--)
            {
                if (lines[i] == "")
                {
                    var resultLines = lines.Take(i + 1).ToList();
                    return new DoubleNodeParser().GetDoubleNode(resultLines);
                }
            }

            throw new FileLoadException();
        }

        private List<string> GetExpectedDoubleNodeLines(string pathToFile)
        {
            Contract.Requires(!String.IsNullOrEmpty(pathToFile));
            Contract.Requires<FileNotFoundException>(File.Exists(pathToFile));
            Contract.Ensures(Contract.Result<List<string>>() != null);

            var lines = File.ReadAllLines(pathToFile).ToList();
            for (int i = lines.Count - 1; i >= 0; i--)
            {
                if (lines[i] == "")
                {
                    return lines.Skip(i).SkipWhile(line => line == "").ToList();
                }
            }

            throw new FileLoadException();
        }
    }
}