using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundTree;
using BoundTree.Nodes;

namespace ConsoleAppForTesting
{
    class Program
    {
        private static void Main(string[] args)
        {
            var firstTree = new Tree(new RootNode());
            firstTree.Add(new GridQuestion(firstTree, new Identificator(new[] {1})));
            firstTree.Add(new GridQuestion(firstTree, new Identificator(new[] {2})));
            firstTree.Add(new GridQuestion(firstTree, new Identificator(new[] {3})));
            firstTree.Add(new GridQuestion(firstTree, new Identificator(new[] {2,3})));
            var mainNode = firstTree.GetByIdentificator(new Identificator(new[] {2,3}));

            var secondTree = new Tree(new RootNode());
            secondTree.Add(new GridQuestion(secondTree, new Identificator(new[] { 1 })));
            secondTree.Add(new GridQuestion(secondTree, new Identificator(new[] { 2 })));
            secondTree.Add(new GridQuestion(secondTree, new Identificator(new[] { 3 })));
            secondTree.Add(new GridQuestion(secondTree, new Identificator(new[] { 1, 1 })));
            var minorNode = secondTree.GetByIdentificator(new Identificator(new[] { 1, 1 }));
            Console.WriteLine(mainNode.BindWith(minorNode));
            
        }
    }
}
