using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using BoundTree.Logic;

namespace BoundTree.Helpers.ConsoleHelper
{
    public class ConsoleTreeWriter<T> where T : class, IEquatable<T>, new()
    {
        public void WriteToConsole(DoubleNode<T> tree)
        {
            Console.WriteLine(new DoubleNodeConverter<T>().ConvertDoubleNode(tree));
        }

        public void WriteToConsole(SingleTree<T> mainTree, SingleTree<T> minorTree)
        {
            var lines = new SingleTreeConverter<T>().ConvertTrees(mainTree, minorTree);
            var stringBuilder = new StringBuilder();
            lines.ForEach(line => stringBuilder.AppendLine(line));
            Console.WriteLine(stringBuilder);
        }
    }
}