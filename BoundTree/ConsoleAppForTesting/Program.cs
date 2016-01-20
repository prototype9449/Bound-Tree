using System;
using BoundTree.ConsoleDisplaying;
using BoundTree.Helpers;
using BoundTree.Logic;
using BoundTree.Logic.LogicLevelProviders;
using BoundTree.TreeReconstruction;


namespace ConsoleAppForTesting
{
    public static class Program
    {
        [STAThreadAttribute]
        private static void Main(string[] args)
        {   
            var buildingTreeLogicLevelProvider = new BuildingTreeLogicLevelProvider();
            var nodeInfoFactory = new NodeInfoFactory(buildingTreeLogicLevelProvider);
            var connectionContructor = new ConnectionContructor<StringId>(nodeInfoFactory);
            var treeContructor = new TreeConstructor<StringId>(nodeInfoFactory, connectionContructor);
            var сonsoleConnectionController = new ConsoleConnectionController(treeContructor, nodeInfoFactory);
            var singleTreeParser = new SingleTreeParser(nodeInfoFactory);
            var multiTreeParser = new MultiTreeParser(treeContructor, singleTreeParser, nodeInfoFactory);
            new ConsoleController(сonsoleConnectionController,multiTreeParser, singleTreeParser).Run();
        }
    }
}