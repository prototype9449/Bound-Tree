using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Logic;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;

namespace BoundTree.Helpers
{
    public class SingleTreeConverter<T> where T : class, IEquatable<T>, new()
    {
        public List<string> ConvertTrees(SingleTree<T> mainSingleTree, SingleTree<T> minorSingleTree)
        {
            Contract.Requires(mainSingleTree != null);
            Contract.Requires(minorSingleTree != null);

            const int spaceBetweenTrees = 10;
            const char signBetweenTrees = ' ';

            var firstTreeLines = ConvertTree(mainSingleTree);
            var secondTreeLines = ConvertTree(minorSingleTree);

            var lines = new List<string>();

            var maxlength = Math.Max(firstTreeLines.Count(), secondTreeLines.Count());

            for (int i = 0; i < maxlength; i++)
            {
                var firstPart = i < firstTreeLines.Count
                    ? firstTreeLines[i]
                    : new string(signBetweenTrees, firstTreeLines.First().Length);

                var secondPart = i < secondTreeLines.Count
                    ? secondTreeLines[i]
                    : "";

                lines.Add(firstPart + new String(signBetweenTrees, spaceBetweenTrees) + secondPart);
                lines.Add(Environment.NewLine);
            }
            return lines;
        }

        public List<string> ConvertTree(SingleTree<T> singleTree)
        {
            Contract.Requires(singleTree != null);
            Contract.Ensures(Contract.Result<List<string>>() != null);

            const int indent = 3;
            const char signBetweenTrees = ' ';

            if (singleTree == null)
                throw new ArgumentNullException("singleTree");

            var lines = new List<string>();

            if (singleTree.Root == null)
                return lines;

            var stack = new Stack<SingleNode<T>>(new[] { singleTree.Root });

            while (stack.Any())
            {
                var topElement = stack.Pop();

                var nodes = topElement.Childs.ToList();
                nodes.Reverse();
                nodes.ForEach(node => stack.Push(node));

                var line = string.Format("{0}{1} ({2})", new string(signBetweenTrees, topElement.SingleNodeData.Depth * indent),
                    topElement.SingleNodeData.NodeInfo.GetType().Name, topElement.SingleNodeData.Id);
                lines.Add(line);
            }

            var maxLength = lines.Max(line => line.Length);
            return lines.Select(line => line += new string(signBetweenTrees, maxLength - line.Length)).ToList();
        }
    }
}