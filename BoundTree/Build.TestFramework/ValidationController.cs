using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using BoundTree;
using BoundTree.Helpers.Helpers;
using BoundTree.Logic;
using BoundTree.Logic.Nodes;
using BoundTree.TreeReconstruction;

namespace Build.TestFramework
{
    public class ValidationController
    {
        public DoubleNode<StringId> GetDoubleNodeFromFile(string pathToFile)
        {
            Contract.Requires(!String.IsNullOrEmpty(pathToFile));
            Contract.Requires<FileNotFoundException>(File.Exists(pathToFile));
            Contract.Ensures(Contract.Result<DoubleNode<StringId>>() != null);

            var lines = File.ReadAllLines(pathToFile).ToList();
            return GetDoubleNode(lines);
        }


        public bool IsValid(string pathToFile)
        {
            
        }
    }
}