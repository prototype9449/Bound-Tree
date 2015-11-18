using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using BoundTree.Helpers;
using BoundTree.Helpers.ConsoleHelper;
using BoundTree.Logic;

namespace ConsoleAppForTesting
{
    internal class Program
    {
        private static void Main(string[] args)
        {
//            var treeFactory = new TreeFactory();
//            var mainTree = treeFactory.GetTree(new Tree12());
//            var minorTree = treeFactory.GetTree(new Tree13());
//
//            var bindController = new BindContoller<StringId>(mainTree, minorTree);
//            new ConsoleConnectionController(bindController).Start();

            //new ConsoleController().Run();

            //TreeLogger.GetDoubleNodeFromFile();
            Console.WriteLine(new Regex(@"\[(.*?)\]").Split("(hi)")[0]);
            
        }
    }
}