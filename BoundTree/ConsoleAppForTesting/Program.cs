using System;
using System.Collections.Generic;
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
            Node mainNode = firstTree.GetByIdentificator(new Identificator(new[] {1, 2, 3}));

            var secongBindingHandler = new BindingHandler();
            var secondTree = new Tree(new RootNode(new Identificator(new[] {1}), secongBindingHandler));
            secondTree.Add(new GridQuestion(secongBindingHandler, new Identificator(new[] {1, 1})));
            secondTree.Add(new GridQuestion(secongBindingHandler, new Identificator(new[] {1, 2})));
            secondTree.Add(new GridQuestion(secongBindingHandler, new Identificator(new[] {1, 3})));
            secondTree.Add(new GridQuestion(secongBindingHandler, new Identificator(new[] {1, 1, 1})));
            Node minorNode = secondTree.GetByIdentificator(new Identificator(new[] {1, 1, 1}));
            Console.WriteLine(mainNode.BindWith(minorNode));
            List<Node> nodes = firstTree.ToList();
        }
    }
}