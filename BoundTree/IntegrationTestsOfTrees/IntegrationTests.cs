using System;
using System.IO;
using BoundTree.Helpers;
using BoundTree.Logic;
using BoundTree.Logic.LogicLevelProviders;
using BoundTree.TreeReconstruction;
using Build.TestFramework;
using NUnit.Framework;

namespace IntegrationTestsOfTrees
{
    [TestFixture]
    public class IntegrationTests
    {
        private string _pathToTestFolder;
        private ValidationController _validator;
        public IntegrationTests()
        {
            var logicLevelProvider = new ConstructionTreeLogicLevelProvider();
            var nodeInfoFactory = new NodeInfoFactory(logicLevelProvider);
            var connectionContructor = new ConnectionContructor<StringId>(nodeInfoFactory);
            var treeContstruector = new TreeConstructor<StringId>(nodeInfoFactory, connectionContructor);
            var singleTreeParser = new SingleTreeParser(nodeInfoFactory);

            _validator = new ValidationController(new MultiTreeParser(treeContstruector, singleTreeParser, nodeInfoFactory));
            _pathToTestFolder = Path.Combine("C:\\Bound-Tree\\BoundTree\\IntegrationTestsOfTrees","Tests");
        }

        private string GetFullPath(string fileName)
        {
            return Path.Combine(_pathToTestFolder, fileName);
        }

        [TestCase("test1.txt")]
        [TestCase("test2.txt")]
        [TestCase("test3.txt")]
        [TestCase("test4.txt")]
        [TestCase("test5.txt")]
        [TestCase("test6.txt")]
        [TestCase("test7.txt")]
        [TestCase("test8.txt")]
        [TestCase("test9.txt")]
        [TestCase("test10.txt")]
        [TestCase("test11.txt")]
        [TestCase("test12.txt")]
        [TestCase("test13.txt")]
        [TestCase("test14.txt")]
        [TestCase("test15.txt")]
        [TestCase("test16.txt")]
        [TestCase("test17.txt")]
        public void TestBuildingTree(string path)
        {
            Assert.IsTrue(_validator.IsValid(GetFullPath(path)));
        }
    }
}
