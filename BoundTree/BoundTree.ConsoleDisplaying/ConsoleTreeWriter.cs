using System;
using System.Text;
using BoundTree.Helpers;
using BoundTree.Logic;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;

namespace BoundTree.ConsoleDisplaying
{
    public class ConsoleTreeWriter
    {
        private readonly TreeConverter<StringId> _singleTreeConverter = new TreeConverter<StringId>();

        public string ConvertToString(DoubleNode<StringId> tree)
        {
            var lines = new DoubleNodeConverter().ConvertDoubleNode(tree);
            var stringBuilder = new StringBuilder();
            lines.ForEach(line => stringBuilder.AppendLine(line));
            return stringBuilder.ToString();
        }

        public string ConvertToString(SingleTree<StringId> mainTree, SingleTree<StringId> minorTree)
        {
            var lines = _singleTreeConverter.ConvertTrees(mainTree, minorTree);
            var stringBuilder = new StringBuilder();
            lines.ForEach(line => stringBuilder.AppendLine(line));
            return stringBuilder.ToString();
        }

        public string ConvertToString(MultiTree<StringId> mainTree, SingleTree<StringId> minorTree)
        {
            var lines = _singleTreeConverter.ConvertTrees(mainTree, minorTree);
            var stringBuilder = new StringBuilder();
            lines.ForEach(line => stringBuilder.AppendLine(line));
            return stringBuilder.ToString();
        }
    }
}