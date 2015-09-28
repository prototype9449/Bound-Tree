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

            var firstBindingHandler = new BindingHandler();

            var firstTree = GetMainTree(firstBindingHandler);
            Node mainNode1 = firstTree.GetById(new Identificator(new[] {1, 2, 2}));
            Node mainNode2 = firstTree.GetById(new Identificator(new[] {1, 3, 1}));
            Node mainNode3 = firstTree.GetById(new Identificator(new[] {1, 3, 1, 2}));

            var minorTree = GetMinorTree(firstBindingHandler);
            Node minorNode1 = minorTree.GetById(new Identificator(new[] { 1, 3, 2 }));
            Node minorNode2 = minorTree.GetById(new Identificator(new[] { 1, 1 }));
            Node minorNode3 = minorTree.GetById(new Identificator(new[] { 1, 3, 1 }));

            Console.WriteLine(mainNode1.BindWith(minorNode1));
            Console.WriteLine(mainNode2.BindWith(minorNode2));
            Console.WriteLine(mainNode3.BindWith(minorNode3));

           
            var ids = firstBindingHandler.BoundNodes.Select(pair => pair.Value).ToList();
            var newMinorTree = new TreeBuilder().BuildTreeByEnds(minorTree, ids);
            var pairTrees = new TreeStabilizer().GetBalancedTrees(firstTree, newMinorTree, firstBindingHandler);
            var newFirstTree = pairTrees.Key;
            var newSecondTree = pairTrees.Value;
            Display(newFirstTree, newSecondTree);


        }

        public static Tree GetMainTree(BindingHandler bindingHandler)
        {
            var tree = new Tree(new RootNode(new Identificator(new[] { 1 }), bindingHandler));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 1 })));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 2 })));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 2, 1 })));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 2, 2 })));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 3 })));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 3, 1 })));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 3, 1, 1 })));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 3, 1, 2 })));
            return tree;
        }

        public static Tree GetMinorTree(BindingHandler bindingHandler)
        {
            var tree = new Tree(new RootNode(new Identificator(new[] { 1 }), bindingHandler));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 1 })));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 2 })));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 3 })));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 3, 1 })));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 3, 1, 1 })));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 3, 1, 2 })));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 3, 1, 3 })));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 3, 2 })));
            tree.Add(new GridQuestion(bindingHandler, new Identificator(new[] { 1, 3, 2, 1 })));
            return tree;
        }

        public static void Display(Tree firtsTree, Tree secondTree)
        {
            var firstTreeList = firtsTree.ToList();
            var secondTreeList = secondTree.ToList();

            for (var i = 0; i < firstTreeList.Count; i++)
            {
                Console.WriteLine("{0}  --  {1}", firstTreeList[i].TestProperty, secondTreeList[i].TestProperty);
            }
        }
    }
}