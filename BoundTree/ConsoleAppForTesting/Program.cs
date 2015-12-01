using System;
using BoundTree.ConsoleDisplaying;
using Build.TestFramework;


namespace ConsoleAppForTesting
{
    internal class Program
    {
        [STAThreadAttribute]
        private static void Main(string[] args)
        {
//            var treeFactory = new TreeFactory();
//            var mainTree = treeFactory.GetTree(new Tree12());
//            var minorTree = treeFactory.GetTree(new Tree13());
//
//            var bindController = new BindContoller<StringId>(mainTree, minorTree);
//            new ConsoleConnectionController(bindController).Start();

            new ConsoleController().Run();
            
            //Console.WriteLine(new ValidationController().IsValid(@"C:\Bound-Tree\BoundTree\ConsoleAppForTesting\bin\Debug\test.txt"));
        }
    }
}