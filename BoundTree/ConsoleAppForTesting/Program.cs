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
            var firstTree = new Tree(new RootNode(new Identificator(new[] {1}), firstBindingHandler));
            firstTree.Add(new GridQuestion(firstBindingHandler, new Identificator(new[] {1, 1})));
            firstTree.Add(new GridQuestion(firstBindingHandler, new Identificator(new[] {1, 2})));
            firstTree.Add(new GridQuestion(firstBindingHandler, new Identificator(new[] {1, 3})));
            firstTree.Add(new GridQuestion(firstBindingHandler, new Identificator(new[] {1, 2, 3})));
            Node mainNode1 = firstTree.GetByIdentificator(new Identificator(new[] {1, 1}));
            Node mainNode2 = firstTree.GetByIdentificator(new Identificator(new[] {1, 2}));

            var secongBindingHandler = new BindingHandler();
            var secondTree = new Tree(new RootNode(new Identificator(new[] {1}), secongBindingHandler));
            secondTree.Add(new GridQuestion(secongBindingHandler, new Identificator(new[] {1, 1})));
            secondTree.Add(new GridQuestion(secongBindingHandler, new Identificator(new[] {1, 2})));
            secondTree.Add(new GridQuestion(secongBindingHandler, new Identificator(new[] {1, 3})));
            secondTree.Add(new GridQuestion(secongBindingHandler, new Identificator(new[] {1, 1, 1})));
            Node minorNode1 = secondTree.GetByIdentificator(new Identificator(new[] {1, 1}));
            Node minorNode2 = secondTree.GetByIdentificator(new Identificator(new[] {1, 2}));
            Console.WriteLine(mainNode1.BindWith(minorNode1));
            Console.WriteLine(mainNode2.BindWith(minorNode2));
            var treeBuilder = new TreeBuilder(secondTree);
            var ids = firstBindingHandler.BoundNodes.Select(pair => pair.Value).ToList();
            var newTree = treeBuilder.BuildTree(ids);
        }
    }
}