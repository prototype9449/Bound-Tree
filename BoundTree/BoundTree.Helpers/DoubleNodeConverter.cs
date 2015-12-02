using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Logic;
using BoundTree.Logic.TreeNodes;

namespace BoundTree.Helpers
{
    public class DoubleNodeConverter
    {
        private const int TabSpace = 2;
        private const string StrictConnectionSign = "+";
        private const string RelativeConnectionSign = "*";
        private const string NoneConnectionSign = " ";

        public List<string> ConvertDoubleNode(DoubleNode<StringId> doubleNode)
        {
            Contract.Requires(doubleNode != null);

            var firstTreeLines = GetNodeLines(doubleNode, true);
            var secondTreeLines = GetNodeLines(doubleNode, false);

            var lines = new List<string>();
            for (int i = 0; i < firstTreeLines.Count; i++)
            {
                lines.Add(firstTreeLines[i] + secondTreeLines[i]);
                //lines.Add(Environment.NewLine);
            }

            return lines;
        }

        private List<string> GetNodeLines(DoubleNode<StringId> doubleNode, bool isLeft)
        {
            Contract.Requires(doubleNode != null);
            Contract.Ensures(Contract.Result<List<string>>().Count != 0);
            Contract.Ensures(Contract.Result<List<string>>() != null);

            var nodeLines = new List<string>();
            var stack = new Stack<DoubleNode<StringId>>();
            stack.Push(doubleNode);

            var maxDepth = doubleNode.ToList().Max(node => node.Deep);

            while (stack.Any())
            {
                var topElement = stack.Pop();
                var space = isLeft
                    ? new string(' ', topElement.Deep*TabSpace)
                    : new string(' ', (maxDepth - topElement.Deep)*TabSpace);

                var connectionSign = GetConnectionSigh(topElement.ConnectionKind);

                var mainId = topElement.MainLeaf.Id.ToString() == ""
                    ? "()"
                    : topElement.MainLeaf.Id.ToString();

                var minorId = topElement.MinorLeaf.Id.ToString() == ""
                    ? "()"
                    : topElement.MinorLeaf.Id.ToString();

                var line = isLeft
                    ? space + mainId + new string(' ', TabSpace)
                    : connectionSign + new string(' ', TabSpace) + space + minorId;

                nodeLines.Add(line);

                var nodes = topElement.Nodes.ToList();
                nodes.Reverse();
                nodes.ForEach(node => stack.Push(node));
            }

            var maxLength = nodeLines.Max(line => line.Length);

            nodeLines = nodeLines.Select(line => line += new String(' ', maxLength - line.Length)).ToList();

            return nodeLines;
        }

        private string GetConnectionSigh(ConnectionKind connectionKind)
        {
            Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));

            if (connectionKind == ConnectionKind.None)
                return NoneConnectionSign;

            return connectionKind == ConnectionKind.Strict ? StrictConnectionSign : RelativeConnectionSign;
        }
    }
}