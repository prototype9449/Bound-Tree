using System;
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
//            var mainTree = treeFactory.GetTree(new Tree10());
//            var minorTree = treeFactory.GetTree(new Tree11());
//
//            var bindController = new BindContoller<StringId>(mainTree, minorTree);
//            new ConsoleConnectionController(bindController).Start();

            new ConsoleController().Run();
        }
    }
}