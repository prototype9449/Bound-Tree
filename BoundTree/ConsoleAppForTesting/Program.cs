using System;
using BoundTree;
using BoundTree.Helpers;
using BoundTree.Nodes;

namespace ConsoleAppForTesting
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bindingHandler = new BindingHandler<string>();
            var mainTree = GetMainTree(bindingHandler);
            var minorTree = GetMinorTree(bindingHandler);

            var mainVertex3 = mainTree.GetById("A");
            var mainVertex4 = mainTree.GetById("B");
            var mainVertex8 = mainTree.GetById("C");
            var mainVertex9 = mainTree.GetById("D");

            var minorVertex3 = minorTree.GetById("A");
            var minorVertex4 = minorTree.GetById("B");
            var minorVertex8 = minorTree.GetById("C");
            var minorVertex9 = minorTree.GetById("D");

            mainVertex3.BindWith(minorVertex4);
            mainVertex4.BindWith(minorVertex3);
            mainVertex8.BindWith(minorVertex8);
            mainVertex9.BindWith(minorVertex9);

            var newTree = new TreeFiller<string> ().GetFilledTree(mainTree, minorTree, bindingHandler);
            new ConsoleTreeWriter<string>().WriteToConsoleAsTrees(mainTree, newTree, bindingHandler);
            new ConsoleTableWriter<string>().WriteToConsoleAsTables(mainTree, newTree, bindingHandler);
        }

        public static Tree<string> GetMainTree(BindingHandler<string> bindingHandler)
        {
            var fabrica = new NodeInfoFabrica();

            var tree = new Tree<string>(new Node<string>("A", fabrica.Root, bindingHandler, new[]
            {
                new Node<string>("B", fabrica.SingleQustion, bindingHandler),
                new Node<string>("C", fabrica.GridQuestion,bindingHandler, new[]
                {
                    new Node<string>("D", fabrica.OpenTextInfo, bindingHandler),
                    new Node<string>("E", fabrica.SingleQustion, bindingHandler)
                }),
                new Node<string>("F", fabrica.GridQuestion,bindingHandler, new[]
                {
                    new Node<string>("G",fabrica.SingleQustion, bindingHandler),
                    new Node<string>("H",fabrica.GridQuestion, bindingHandler, new[]
                    {
                        new Node<string>("T",fabrica.SingleQustion, bindingHandler),
                        new Node<string>("R", fabrica.SingleQustion,bindingHandler)
                    })
                })
            }));

            return tree;
        }

        public static Tree<string> GetMinorTree(BindingHandler<string> bindingHandler)
        {
            var fabrica = new NodeInfoFabrica();

            var tree = new Tree<string>(new Node<string>("A", fabrica.Root, bindingHandler, new[]
            {
                new Node<string>("B", fabrica.SingleQustion, bindingHandler),
                new Node<string>("C", fabrica.GridQuestion,bindingHandler, new[]
                {
                    new Node<string>("D", fabrica.OpenTextInfo, bindingHandler),
                    new Node<string>("E", fabrica.SingleQustion, bindingHandler)
                }),
                new Node<string>("F", fabrica.GridQuestion,bindingHandler, new[]
                {
                    new Node<string>("G",fabrica.SingleQustion, bindingHandler),
                    new Node<string>("H",fabrica.GridQuestion, bindingHandler, new[]
                    {
                        new Node<string>("T",fabrica.SingleQustion, bindingHandler),
                        new Node<string>("R", fabrica.SingleQustion,bindingHandler)
                    })
                })
            }));

            return tree;
        }
    }
}