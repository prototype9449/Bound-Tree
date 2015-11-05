﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoundTree.Helpers
{
    public class ConsoleTreeWriter<T> where T : class, IEquatable<T>
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
            for (int i = 0; i < firstTreeLines.Count; i++)
            {
                var firstPart = i < firstTreeLines.Count
                    ? firstTreeLines[i]
                    : new string(' ', firstTreeLines.First().Length);

                var secondPart = i < secondTreeLines.Count ? secondTreeLines[i] : "";

                stringBuilder.AppendLine(firstPart + new String(' ', 10) + secondPart);
                stringBuilder.AppendLine();
            }
            Console.WriteLine(stringBuilder);
        }

        private List<string> GetNodeLines(SingleTree<T>  singleTree)
        {
            var stack = new Stack<SingleNode<T>>(new []{ singleTree.Root });
            var lines = new List<string>();

            while (stack.Any())
            {
                var topElement = stack.Pop();

                topElement.Nodes.ForEach(node => stack.Push(node));

                var line = string.Format("{0}{1} ({2})", new string(' ', topElement.Deep*2),
                    topElement.NodeInfo.GetType().Name, topElement.Id);
                lines.Add(line);
            }

            var maxLength = lines.Max(line => line.Length);
            return lines.Select(line => line += new string(' ', maxLength - line.Length)).ToList();
        }
        
        private List<string> GetNodeLines(DoubleNode<T>  tree, bool isLeft)
        {
            var nodeLines = new List<string>();
            var stack = new Stack<DoubleNode<T>>();
            stack.Push(tree);

            Func<DoubleNode<T>, string> getNodeName = (node) => isLeft 
                ? node.MainLeaf.NodeInfo.GetType().Name + '(' + node.MainLeaf.Id + ')' 
                : "(" + node.MinorLeaf.Id + ')' + node.MinorLeaf.NodeInfo.GetType().Name;

            while (stack.Any())
            {
                var topElement = stack.Pop();

                var maxDeep = tree.ToList().Max(node => node.Deep);

                var space = isLeft ? new string(' ', topElement.Deep * 3) : new String('-', (maxDeep - topElement.Deep) * 3);

                var additionalSeparator = topElement.ConnectionKind == ConnectionKind.Strict ? isLeft ? '<' : '>' : '-';
                var line = isLeft
                    ? space + getNodeName(topElement) + additionalSeparator
                    : space + additionalSeparator + getNodeName(topElement);

                nodeLines.Add(line);

                foreach (var node in topElement.Nodes.OrderByDescending(node => isLeft ? node.MainLeaf.Id : node.MinorLeaf.Id))
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