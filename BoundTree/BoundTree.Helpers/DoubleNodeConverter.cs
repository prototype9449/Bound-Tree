using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Logic;

namespace BoundTree.Helpers
{
    public class DoubleNodeConverter
    {
        private const int TabSpace = 3;
        private const string LeftStrictConnectionSign = " << ";
        private const string RightStrictConnectionSign = " >> ";
        private const string LeftRelativeConnectionSign = " <* ";
        private const string RightRelativeConnectionSign = " *> ";
        private const string NoneConnectionSign = "----";
        private const string ConnectionSeparator = "-";

        public List<string> ConvertDoubleNode(DoubleNode<StringId> doubleNode)
        {
            Contract.Requires(doubleNode != null);

            var firstTreeLines = GetNodeLines(doubleNode, true);
            var secondTreeLines = GetNodeLines(doubleNode, false);

            var lines = new List<string>();
            for (int i = 0; i < firstTreeLines.Count; i++)
            {
                lines.Add(firstTreeLines[i] + ConnectionSeparator + secondTreeLines[i]);
                lines.Add(Environment.NewLine);
            }

            return lines;
        }
        
        private string GetDoubleNodeName(DoubleNode<StringId> doubleNode, bool isLeft)
        {
            if (isLeft)
            {
                return String.Format("{0} ({1})", doubleNode.MainLeaf.NodeType.Name, doubleNode.MainLeaf.Id);
            }

            return String.Format("{0} ({1})", doubleNode.MinorLeaf.NodeType.Name, doubleNode.MinorLeaf.Id);
        }

        private List<string> GetNodeLines(DoubleNode<StringId> doubleNode, bool isLeft)
        {
            Contract.Requires(doubleNode != null);
            Contract.Ensures(Contract.Result<List<string>>().Count != 0);
            Contract.Ensures(Contract.Result<List<string>>() != null);

            var nodeLines = new List<string>();
            var stack = new Stack<DoubleNode<StringId>>();
            stack.Push(doubleNode);


            while (stack.Any())
            {
                var topElement = stack.Pop();
                var space = isLeft
                    ? new string(' ', topElement.Deep * TabSpace)
                    : new string('-', topElement.Deep * TabSpace);

                var connectionSign = GetConnectionSigh(topElement.ConnectionKind, isLeft);

                var line = isLeft
                    ? space + GetDoubleNodeName(topElement, true) + connectionSign
                    : space + connectionSign + GetDoubleNodeName(topElement, false);

                nodeLines.Add(line);

                var nodes = topElement.Nodes.ToList();
                nodes.Reverse();
                nodes.ForEach(node => stack.Push(node));
            }

            var maxLength = nodeLines.Max(line => line.Length);

            nodeLines =
                nodeLines.Select(
                    line => isLeft
                            ? line += new String('-', maxLength - line.Length)
                            : line += new String(' ', maxLength - line.Length)
                                ).ToList();

            return nodeLines;
        }

        private string GetConnectionSigh(ConnectionKind connectionKind, bool isLeft)
        {
            Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));

            if (connectionKind == ConnectionKind.Strict)
            {
                return isLeft ? LeftStrictConnectionSign : RightStrictConnectionSign;
            }

            if (connectionKind == ConnectionKind.Relative)
            {
                return isLeft ? LeftRelativeConnectionSign : RightRelativeConnectionSign;
            }

            return isLeft ? " " + NoneConnectionSign : NoneConnectionSign + " ";
        }
    }
}