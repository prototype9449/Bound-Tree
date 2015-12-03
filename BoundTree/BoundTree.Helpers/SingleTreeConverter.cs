using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using BoundTree.Logic;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;

namespace BoundTree.Helpers
{
    public class SingleTreeConverter<T> where T : class, IEquatable<T>, new()
    {
        private const char SignBetweenTrees = ' ';
        private const int SpaceBetweenTrees = 10;

        public List<string> ConvertTrees(MultiTree<T> mainSingleTree, SingleTree<T> minorSingleTree)
        {
            Contract.Requires(mainSingleTree != null);
            Contract.Requires(minorSingleTree != null);

            var firstTreeLines = ConvertMultiTree(mainSingleTree);
            var secondTreeLines = ConvertSingleTree(minorSingleTree);

            return ConcatenateAsTreeLines(firstTreeLines, secondTreeLines);
        }

        public List<string> ConvertTrees(SingleTree<T> mainSingleTree, SingleTree<T> minorSingleTree)
        {
            Contract.Requires(mainSingleTree != null);
            Contract.Requires(minorSingleTree != null);

            var firstTreeLines = ConvertSingleTree(mainSingleTree);
            var secondTreeLines = ConvertSingleTree(minorSingleTree);

            return ConcatenateAsTreeLines(firstTreeLines, secondTreeLines);
        }

        private static List<string> ConcatenateAsTreeLines(List<string> firstTreeLines, List<string> secondTreeLines)
        {
            var lines = new List<string>();

            var maxlength = Math.Max(firstTreeLines.Count(), secondTreeLines.Count());

            for (int i = 0; i < maxlength; i++)
            {
                var firstPart = i < firstTreeLines.Count
                    ? firstTreeLines[i]
                    : new string(SignBetweenTrees, firstTreeLines.First().Length);

                var secondPart = i < secondTreeLines.Count
                    ? secondTreeLines[i]
                    : "";

                lines.Add(firstPart + new String(SignBetweenTrees, SpaceBetweenTrees) + secondPart);
                lines.Add(Environment.NewLine);
            }
            return lines;
        }

        public List<string> ConvertMultiTree(MultiTree<T> multiTree)
        {
            Contract.Requires(multiTree != null);
            Contract.Requires(multiTree.Root != null);

            const int indent = 4;

            var lines = new List<string>();

            var stack = new Stack<MultiNode<T>>(new[] { multiTree.Root });

            while (stack.Any())
            {
                var topElement = stack.Pop();

                var nodes = topElement.Childs.ToList();
                nodes.Reverse();
                nodes.ForEach(node => stack.Push(node));

                var fullIdChain = new StringBuilder(topElement.Id + " :");
                 topElement.MultiNodeData.MinorDataNodes.ForEach(id => fullIdChain.AppendFormat("|{0}|", id));
                

                var line = string.Format("{0}{1} ({2})", new string(SignBetweenTrees, topElement.Depth * indent),
                    topElement.NodeType.Name, fullIdChain);
                lines.Add(line);
            }

            var maxLength = lines.Max(line => line.Length);
            return lines.Select(line => line += new string(SignBetweenTrees, maxLength - line.Length)).ToList();
        }

        public List<string> ConvertSingleTree(SingleTree<T> singleTree)
        {
            Contract.Requires(singleTree != null);
            Contract.Ensures(Contract.Result<List<string>>() != null);

            const int indent = 3;
            
            var lines = new List<string>();

            var stack = new Stack<SingleNode<T>>(new[] { singleTree.Root });

            while (stack.Any())
            {
                var topElement = stack.Pop();

                var nodes = topElement.Childs.ToList();
                nodes.Reverse();
                nodes.ForEach(node => stack.Push(node));

                var line = string.Format("{0}{1} ({2})", new string(SignBetweenTrees, topElement.Depth * indent),
                    topElement.NodeType.Name, topElement.Id);
                lines.Add(line);
            }

            var maxLength = lines.Max(line => line.Length);
            return lines.Select(line => line += new string(SignBetweenTrees, maxLength - line.Length)).ToList();
        }
    }
}