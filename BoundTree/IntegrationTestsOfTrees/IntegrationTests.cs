using System;
using System.IO;
using Build.TestFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestsOfTrees
{
    [TestClass]
    public class IntegrationTests
    {
        private string _pathToTestFolder;
        private ValidationController _validator = new ValidationController();
        public IntegrationTests()
        {
            _pathToTestFolder = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName,"Tests");
        }

        [TestMethod]
        public void TestTxt1()
        {
            var path = Path.Combine(_pathToTestFolder, "test1.txt");
            Assert.IsTrue(_validator.IsValid(path));
        }

        [TestMethod]
        public void TestTxt2()
        {
            var path = Path.Combine(_pathToTestFolder, "test2.txt");
            Assert.IsTrue(_validator.IsValid(path));
        }

        [TestMethod]
        public void TestTxt3()
        {
            var path = Path.Combine(_pathToTestFolder, "test3.txt");
            Assert.IsTrue(_validator.IsValid(path));
        }

        [TestMethod]
        public void TestTxt4()
        {
            var path = Path.Combine(_pathToTestFolder, "test4.txt");
            Assert.IsTrue(_validator.IsValid(path));
        }

        [TestMethod]
        public void TestTxt5()
        {
            var path = Path.Combine(_pathToTestFolder, "test5.txt");
            Assert.IsTrue(_validator.IsValid(path));
        }

        [TestMethod]
        public void TestTxt6()
        {
            var path = Path.Combine(_pathToTestFolder, "test6.txt");
            Assert.IsTrue(_validator.IsValid(path));
        }

        [TestMethod]
        public void TestTxt7()
        {
            var path = Path.Combine(_pathToTestFolder, "test7.txt");
            Assert.IsTrue(_validator.IsValid(path));
        }

        [TestMethod]
        public void TestTxt8()
        {
            var path = Path.Combine(_pathToTestFolder, "test8.txt");
            Assert.IsTrue(_validator.IsValid(path));
        }
    }
}
