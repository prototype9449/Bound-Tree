using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoundTree.Logic;

namespace BoundTree.Helpers
{
    public class ConsoleTreeWriter<T> where T : class, IEquatable<T>, new()
    {
        public void WriteToConsoleAsTrees(DoubleNode<T> tree)
        {
            var firstTreeLines = GetNodeLines(tree, true);
            var secondTreeLines = GetNodeLines(tree, false);

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < firstTreeLines.Count; i++)
            {
                stringBuilder.AppendLine(firstTreeLines[i] + '-' + secondTreeLines[i]);
                stringBuilder.AppendLine();
            }
            Console.WriteLine(stringBuilder);
        }

        public void WriteToConsoleAsTrees(SingleTree<T> mainSingleTree, SingleTree<T> minorSingleTree)
        {
            var firstTreeLines = GetNodeLines(mainSingleTree);
            var secondTreeLines = GetNodeLines(minorSingleTree);

            var stringBuilder = new StringBuilder();
            var maxlength = Math.Max(firstTreeLines.Count(), secondTreeLines.Count());

            for (int i = 0; i < maxlength; i++)
            {
                var firstPart = i < firstTreeLines.Count
                    ? firstTreeLines[i]
                    : new string(' ', firstTreeLines.First().Length);

                var secondPart = i < secondTreeLines.Count
                    ? secondTreeLines[i]
                    : "";

                stringBuilder.AppendLine(firstPart + new String(' ', 10) + secondPart);
                stringBuilder.AppendLine();
            }
            Console.WriteLine(stringBuilder);
        }

        private List<string> GetNodeLines(SingleTree<T> singleTree)
        {
            var stack = new Stack<SingleNode<T>>(new[] { singleTree.Root });
            var lines = new List<string>();

            while (stack.Any())
            {
                var topElement = stack.Pop();

                var nodes = topElement.Nodes.ToList();
                nodes.Reverse();
                nodes.ForEach(node => stack.Push(node));

                var line = string.Format("{0}{1} ({2})", new string(' ', topElement.Node.Deep * 2),
                    topElement.Node.NodeInfo.GetType().Name, topElement.Node.Id);
                lines.Add(line);
            }

            var maxLength = lines.Max(line => line.Length);
            return lines.Select(line => line += new string(' ', maxLength - line.Length)).ToList();
        }

        private List<string> GetNodeLines(DoubleNode<T> tree, bool isLeft)
        {
            var nodeLines = new List<string>();
            var stack = new Stack<DoubleNode<T>>();
            stack.Push(tree);

            Func<DoubleNode<T>, string> getNodeName = (node) => isLeft
                ? node.MainLeaf.NodeType.Name + '(' + node.MainLeaf.Id + ')'
                : "(" + node.MinorLeaf.Id + ')' + node.MinorLeaf.NodeType.Name;

            while (stack.Any())
            {
                var topElement = stack.Pop();

                var maxDeep = tree.ToList().Max(node => node.Deep);

                var space = isLeft
                    ? new string(' ', topElement.Deep * 3)
                    : new String('-', (maxDeep - topElement.Deep) * 3);               

                var line = isLeft
                    ? space + getNodeName(topElement) + topElement.Connection.GetSign(true)
                    : space + topElement.Connection.GetSign(false) + getNodeName(topElement);

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
    }
}