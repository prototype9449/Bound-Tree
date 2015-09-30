using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree;
using BoundTree.Helpers;
using BoundTree.Nodes;
using BoundTree.Nodes.Questions;

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

            mainVertex3.BindWith(minorVertex3);
            mainVertex4.BindWith(minorVertex4);
            mainVertex8.BindWith(minorVertex8);
            mainVertex9.BindWith(minorVertex9);

            var mainParent = mainTree.GetParent(0);
            var connections = bindingHandler.BoundNodes;
            var newTree = mainTree.Clone();

        }

        public static Tree GetMainTree(BindingHandler bindingHandler)
        {
            var tree = new Tree(new RootNode(0, bindingHandler, new Node[]
            {
                new SingleQuestion(1, bindingHandler),
                new GridQuestion(2, bindingHandler, new Node[]
                {
                    new SingleQuestion(3, bindingHandler),
                    new SingleQuestion(4, bindingHandler)
                }),
                new GridQuestion(5, bindingHandler, new Node[]
                {
                    new SingleQuestion(6, bindingHandler),
                    new GridQuestion(7, bindingHandler, new Node[]
                    {
                        new SingleQuestion(8, bindingHandler),
                        new SingleQuestion(9, bindingHandler)
                    })
                })
            }));

            return tree;
        }

        public static Tree GetMinorTree(BindingHandler bindingHandler)
        {
            var tree = new Tree(
                new RootNode(0, bindingHandler, new List<Node>
                {
                    new SingleQuestion(1, bindingHandler),
                    new GridQuestion(2, bindingHandler, new Node[]
                    {
                        new SingleQuestion(3, bindingHandler),
                        new SingleQuestion(4, bindingHandler)
                    }),
                    new GridQuestion(5, bindingHandler, new Node[]
                    {
                        new SingleQuestion(6, bindingHandler),
                        new GridQuestion(7, bindingHandler, new Node[]
                        {
                            new SingleQuestion(8, bindingHandler),
                            new SingleQuestion(9, bindingHandler)
                        })
                    })
                }));

            return tree;
        }

        public static void Display(Tree firtsTree, Tree secondTree)
        {
            var firstTreeList = firtsTree.ToList();
            var secondTreeList = secondTree.ToList();

            for (var i = 0; i < firstTreeList.Count; i++)
            {
                Console.WriteLine("{0}  --  {1}", firstTreeList[i].Id, secondTreeList[i].Id);
            }
        }
    }
}