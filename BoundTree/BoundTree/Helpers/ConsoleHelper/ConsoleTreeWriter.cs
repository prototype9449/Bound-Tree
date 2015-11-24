using System;
using System.Text;
using BoundTree.Logic;

namespace BoundTree.Helpers.ConsoleHelper
{
    public class ConsoleTreeWriter
    {
        public void WriteToConsole(DoubleNode<StringId> tree)
        {
            var lines = new DoubleNodeConverter().ConvertDoubleNode(tree);
            var stringBuilder = new StringBuilder();
            lines.ForEach(line => stringBuilder.AppendLine(line));
            Console.WriteLine(stringBuilder);
        }

        public void WriteToConsole(SingleTree<StringId> mainTree, SingleTree<StringId> minorTree)
        {
            var lines = new SingleTreeConverter<StringId>().ConvertTrees(mainTree, minorTree);
            var stringBuilder = new StringBuilder();
            lines.ForEach(line => stringBuilder.AppendLine(line));
            Console.WriteLine(stringBuilder);
        }
    }
}