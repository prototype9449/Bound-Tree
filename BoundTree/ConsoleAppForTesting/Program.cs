using BoundTree;
using BoundTree.Helpers;
using BoundTree.Nodes;

namespace ConsoleAppForTesting
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bindingHandler = new BindingHandler();
            var mainTree = GetMainTree(bindingHandler);
            var minorTree = GetMinorTree(bindingHandler);

            var mainVertex3 = mainTree.GetById(3);
            var mainVertex4 = mainTree.GetById(4);
            var mainVertex8 = mainTree.GetById(8);
            var mainVertex9 = mainTree.GetById(9);

            var minorVertex3 = minorTree.GetById(3);
            var minorVertex4 = minorTree.GetById(4);
            var minorVertex8 = minorTree.GetById(8);
            var minorVertex9 = minorTree.GetById(9);

            mainVertex3.BindWith(minorVertex4);
            mainVertex4.BindWith(minorVertex3);
            mainTree.GetById(0).BindWith(minorTree.GetById(0));
            mainVertex8.BindWith(minorVertex8);
            mainVertex9.BindWith(minorVertex9);

            var newTree = new TreeFiller().GetFilledTree(mainTree, minorTree, bindingHandler);
            new ConsoleTreeWriter().WriteToConsoleAsTrees(mainTree, newTree, bindingHandler);
            new ConsoleTableWriter().WriteToConsoleAsTables(mainTree, newTree, bindingHandler);
        }

        public static Tree GetMainTree(BindingHandler bindingHandler)
        {
            var fabrica = new NodeInfoFabrica();

            var tree = new Tree(new Node(0, fabrica.Root, bindingHandler, new[]
            {
                new Node(1, fabrica.SingleQustion, bindingHandler),
                new Node(2, fabrica.GridQuestion,bindingHandler, new[]
                {
                    new Node(3, fabrica.OpenTextInfo, bindingHandler),
                    new Node(4, fabrica.SingleQustion, bindingHandler)
                }),
                new Node(5, fabrica.GridQuestion,bindingHandler, new[]
                {
                    new Node(6,fabrica.SingleQustion, bindingHandler),
                    new Node(7,fabrica.GridQuestion, bindingHandler, new[]
                    {
                        new Node(8,fabrica.SingleQustion, bindingHandler),
                        new Node(9, fabrica.SingleQustion,bindingHandler)
                    })
                })
            }));

            return tree;
        }

        public static Tree GetMinorTree(BindingHandler bindingHandler)
        {
            var fabrica = new NodeInfoFabrica();

            var tree = new Tree(new Node(0, fabrica.Root, bindingHandler, new Node[]
            {
                new Node(1, fabrica.SingleQustion, bindingHandler),
                new Node(2, fabrica.GridQuestion,bindingHandler, new Node[]
                {
                    new Node(3, fabrica.SingleQustion, bindingHandler),
                    new Node(4, fabrica.SingleQustion, bindingHandler)
                }),
                new Node(5, fabrica.GridQuestion,bindingHandler, new Node[]
                {
                    new Node(6,fabrica.SingleQustion, bindingHandler),
                    new Node(7,fabrica.GridQuestion, bindingHandler, new Node[]
                    {
                        new Node(8,fabrica.SingleQustion, bindingHandler),
                        new Node(9, fabrica.SingleQustion,bindingHandler)
                    })
                })
            }));

            return tree;
        }
    }
}