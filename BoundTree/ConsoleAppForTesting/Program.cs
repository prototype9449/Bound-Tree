using BoundTree.ConsoleDisplaying;


namespace ConsoleAppForTesting
{
    internal class Programa
    {
        private static void Main(string[] args)
        {
//            var treeFactory = new TreeFactory();
//            var mainTree = treeFactory.GetTree(new Tree12());
//            var minorTree = treeFactory.GetTree(new Tree13());
//
//            var bindController = new BindContoller<StringId>(mainTree, minorTree);
//            new ConsoleConnectionController(bindController).Start();

            new ConsoleController().Run();

            //var doubleNode = TreeLogger.GetDoubleNodeFromFile();
            
        }
    }
}