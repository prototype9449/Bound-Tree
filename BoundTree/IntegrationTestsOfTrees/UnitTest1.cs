using System;
using System.IO;
using Build.TestFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestsOfTrees
{
    [TestClass]
    public class UnitTest1
    {
        private string _pathToTestFolder;
        private ValidationController _validator = new ValidationController();
        public UnitTest1()
        {
            _pathToTestFolder = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName,"Tests");
        }

        [TestMethod]
        public void TestMethod1()
        {
            var path = Path.Combine(_pathToTestFolder, "test1.txt");
            Assert.IsTrue(_validator.IsValid(path));
        }

        [TestMethod]
        public void TestMethod2()
        {
            var path = Path.Combine(_pathToTestFolder, "test2.txt");
            Assert.IsTrue(_validator.IsValid(path));
        }

        [TestMethod]
        public void TestMethod3()
        {
            var path = Path.Combine(_pathToTestFolder, "test3.txt");
            Assert.IsTrue(_validator.IsValid(path));
        }

        [TestMethod]
        public void TestMethod4()
        {
            var path = Path.Combine(_pathToTestFolder, "test4.txt");
            Assert.IsTrue(_validator.IsValid(path));
        }

        [TestMethod]
        public void TestMethod5()
        {
            var path = Path.Combine(_pathToTestFolder, "test5.txt");
            Assert.IsTrue(_validator.IsValid(path));
        }
    }
}
