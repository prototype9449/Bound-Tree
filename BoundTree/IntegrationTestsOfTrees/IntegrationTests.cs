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

        private string GetFullPath(string fileName)
        {
            return Path.Combine(_pathToTestFolder, fileName);
        }

        [TestMethod]
        public void TestTxt1()
        {
            Assert.IsTrue(_validator.IsValid(GetFullPath("test1.txt")));
        }

        [TestMethod]
        public void TestTxt2()
        {
            Assert.IsTrue(_validator.IsValid(GetFullPath("test2.txt")));
        }

        [TestMethod]
        public void TestTxt3()
        {
            Assert.IsTrue(_validator.IsValid(GetFullPath("test3.txt")));
        }

        [TestMethod]
        public void TestTxt4()
        {
            Assert.IsTrue(_validator.IsValid(GetFullPath("test4.txt")));
        }

        [TestMethod]
        public void TestTxt5()
        {
            Assert.IsTrue(_validator.IsValid(GetFullPath("test5.txt")));
        }

        [TestMethod]
        public void TestTxt6()
        {
            Assert.IsTrue(_validator.IsValid(GetFullPath("test6.txt")));
        }

        [TestMethod]
        public void TestTxt7()
        {
            Assert.IsTrue(_validator.IsValid(GetFullPath("test7.txt")));
        }

        [TestMethod]
        public void TestTxt8()
        {
            Assert.IsTrue(_validator.IsValid(GetFullPath("test8.txt")));
        }

        [TestMethod]
        public void TestTxt9()
        {
            Assert.IsTrue(_validator.IsValid(GetFullPath("test9.txt")));
        }

        [TestMethod]
        public void TestTxt10()
        {
            Assert.IsTrue(_validator.IsValid(GetFullPath("test10.txt")));
        }

        [TestMethod]
        public void TestTxt11()
        {
            Assert.IsTrue(_validator.IsValid(GetFullPath("test11.txt")));
        }

        [TestMethod]
        public void TestTxt12()
        {
            Assert.IsTrue(_validator.IsValid(GetFullPath("test12.txt")));
        }

        [TestMethod]
        public void TestTxt13()
        {
            Assert.IsTrue(_validator.IsValid(GetFullPath("test13.txt")));
        }

        [TestMethod]
        public void TestTxt14()
        {
            Assert.IsTrue(_validator.IsValid(GetFullPath("test14.txt")));
        }
    }
}
