using BoundTree.Helpers;
using BoundTree.Logic;

namespace ConsoleAppForTesting
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var treeFactory = new TreeFactory();
            var mainTree = treeFactory.GetTree(new Tree9());
            var minorTree = treeFactory.GetTree(new Tree8());

            var bindController = new BindContoller<StringId>(mainTree, minorTree);
            new ConsoleController(bindController).Start();
        }
    }
}