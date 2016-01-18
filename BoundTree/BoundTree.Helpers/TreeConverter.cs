using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using BoundTree.Interfaces;
using BoundTree.Logic;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;

namespace BoundTree.Helpers
{
    public class TreeConverter<T> where T : class, IID<T>,IEquatable<T>, new()
    {
        private const char SignBetweenTrees = ' ';
        private const int SpaceBetweenTrees = 10;

        public List<string> ConvertAsSingleTrees(MultiTree<T> mainSingleTree, SingleTree<T> minorSingleTree)
        {
            Contract.Requires(mainSingleTree != null);
            Contract.Requires(minorSingleTree != null);

            var firstTreeLines = ConvertMultiTreeAsSingle(mainSingleTree);
            var secondTreeLines = ConvertSingleTree(minorSingleTree);

            return ConcatenateAsTreeLines(firstTreeLines, secondTreeLines);
        }

        public List<string> ConvertMultiTreeAsSingle(MultiTree<T> multiTree)
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

                var line = string.Format("{0}{1} ({2})", new string(SignBetweenTrees, topElement.Depth * indent), topElement.NodeType.Name, topElement.Id);
                lines.Add(line);
            }

            var maxLength = lines.Max(line => line.Length);
            return lines.Select(line => line += new string(SignBetweenTrees, maxLength - line.Length)).ToList();
        }


        public List<string> ConvertMultiTreeAsMulti(MultiTree<T> multiTree)
        {
            Contract.Requires(multiTree != null);
            Contract.Requires(multiTree.Root != null);

            var lines = ConvertMultiTreeAsSingle(multiTree);
            var multiNodes = multiTree.ToList();

            if (multiNodes.Count != lines.Count)
            {
                throw new FileLoadException();
            }

            var countMinorNodes = multiTree.Root.MultiNodeData.MinorDataNodes.Count;
            var allMinorNodes = new List<List<string>>();

            for (int i = 0; i < countMinorNodes; i++)
            {
                var minorNodes = new List<string>();
                foreach (var multiNode in multiNodes)
                {
                    var connectionSign = ConnectionSignHelper.GetConnectionSigh(multiNode.MultiNodeData.MinorDataNodes[i].ConnectionKind);
                    var id = multiNode.MultiNodeData.MinorDataNodes[i].NodeData.Id;
                    string stringId = multiNode.MultiNodeData.MinorDataNodes[i].IsEmpty()  ? "(" + id + ")" : id.ToString();
                    minorNodes.Add(connectionSign + " " + stringId);
                }
                allMinorNodes.Add(minorNodes);
            }

            foreach (var listMinorNodes in allMinorNodes)
            {
                for (int i = 0; i < listMinorNodes.Count; i++)
                {
                    lines[i] += " " + listMinorNodes[i];
                }

                var maxLength = lines.Max(line => line.Length);
                lines = lines.Select(line => line + new String(' ', maxLength - line.Length)).ToList();
            }

            return lines;
        }

        public List<string> ConvertTrees(SingleTree<T> mainSingleTree, SingleTree<T> minorSingleTree)
        {
            Contract.Requires(mainSingleTree != null);
            Contract.Requires(minorSingleTree != null);

            var firstTreeLines = ConvertSingleTree(mainSingleTree);
            var secondTreeLines = ConvertSingleTree(minorSingleTree);

            return ConcatenateAsTreeLines(firstTreeLines, secondTreeLines);
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

        private List<string> ConcatenateAsTreeLines(List<string> firstTreeLines, List<string> secondTreeLines)
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
    }
}