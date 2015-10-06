using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoundTree.Helpers
{
    public class ConsoleTreeWriter
    {
        public void WriteToConsoleAsTrees(Tree firstTree, Tree secondTree, BindingHandler bindingHandler)
        {
            var ids = bindingHandler.BoundNodes.Select(pair => pair.Key).ToList();

            var firstTreeLines = GetNodeLines(firstTree, ids, true);
            var secondTreeLines = GetNodeLines(secondTree, ids, false);

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < firstTreeLines.Count; i++)
            {
                stringBuilder.AppendLine(firstTreeLines[i] + '-' + secondTreeLines[i]);
                stringBuilder.AppendLine();
            }
            Console.WriteLine(stringBuilder);
        }

        private List<string> GetNodeLines(Tree tree, List<int> ids, bool isLeft)
        {
            var nodeLines = new List<string>();
            var stack = new Stack<Node>();
            stack.Push(tree.Root);

            Func<Node, string> getNodeName = (node) => isLeft ? node.NodeInfo.GetType().Name + '(' + node.Id + ')' : "(" + node.Id + ')' + node.NodeInfo.GetType().Name;

            while (stack.Count != 0)
            {
                var topElement = stack.Pop();

                var maxDeep = tree.ToList().Max(node => node.Deep);

                var space = isLeft ? new string(' ', topElement.Deep * 3) : new String('-', (maxDeep - topElement.Deep) * 3);

                var additionalSeparator = ids.Contains(topElement.Id) ? isLeft ? '<' : '>' : '-';
                var line = isLeft
                    ? space + getNodeName(topElement) + additionalSeparator
                    : space + additionalSeparator + getNodeName(topElement);

                nodeLines.Add(line);

                foreach (var node in topElement.Nodes.OrderByDescending(node => node.Id))
                {
                    stack.Push(node);
                }
            }

            var maxLength = nodeLines.Max(line => line.Length);

            nodeLines =
                nodeLines.Select(
                    line =>
                        isLeft
                            ? line += new String('-', maxLength - line.Length)
                            : line += new String(' ', maxLength - line.Length)).ToList();

            return nodeLines;
        } 
    }
}