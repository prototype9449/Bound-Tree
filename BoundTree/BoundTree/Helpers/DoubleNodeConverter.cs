using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using BoundTree.Helpers.ConsoleHelper;
using BoundTree.Helpers.TreeReconstruction;
using BoundTree.Logic;
using BoundTree.Logic.Nodes;

namespace BoundTree.Helpers
{
    public class DoubleNodeConverter<T> where T : class, IEquatable<T>, new()
    {
        private const int SpaceCount = 2;
        private const string LeftStrictConnectionSign = " << ";
        private const string RightStrictConnectionSign = " >> ";
        private const string LeftRelativeConnectionSign = " <* ";
        private const string RightRelativeConnectionSign = " *> ";
        private const string NoneConnectionSign = "-";
        private const string ConnectionSeparator = "-";

        public List<string> ConvertDoubleNode(DoubleNode<T> doubleNode)
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

        public DoubleNode<StringId> GetDoubleNode(List<string> lines)
        {
            Contract.Ensures(Contract.Result<DoubleNode<StringId>>() != null);

            var minorTreeIndex = lines.FindIndex(line => line == "") + SpaceCount;

            if (minorTreeIndex > lines.Count)
            {
                throw new FileLoadException("Minor tree not found");
            }

            var connectionIndex = lines.Skip(minorTreeIndex).ToList().FindIndex(line => line == "") + SpaceCount + minorTreeIndex;
            if (connectionIndex > lines.Count)
            {
                throw new FileLoadException("Connection separator was not found");
            }

            var mainTreeLines = lines.Take(minorTreeIndex - SpaceCount).ToList();
            var minorTreeLines = lines
                .Skip(minorTreeIndex)
                .Take(connectionIndex - minorTreeIndex - SpaceCount).ToList();

            var connectionCommands = lines.Skip(connectionIndex).ToList();

            var singleTreeConverter = new SingleTreeConverter<T>();

            var mainTree = singleTreeConverter.GetSingleTree(mainTreeLines);
            var minorTree = singleTreeConverter.GetSingleTree(minorTreeLines);

            var bindController = new BindContoller<StringId>(mainTree, minorTree);
            AddConnections(bindController, connectionCommands);
            return new TreeReconstruction<StringId>(bindController).GetFilledTree();
        }
        
        private List<string> GetNodeLines(DoubleNode<T> doubleNode, bool isLeft)
        {
            Contract.Requires(doubleNode != null);
            Contract.Ensures(Contract.Result<List<string>>().Count != 0);
            Contract.Ensures(Contract.Result<List<string>>() != null);

            var nodeLines = new List<string>();
            var stack = new Stack<DoubleNode<T>>();
            stack.Push(doubleNode);

            Func<DoubleNode<T>, string> getNodeName = (node) => String.Format("{0} ({1})", node.MainLeaf.NodeType.Name, node.MainLeaf.Id);

            while (stack.Any())
            {
                var topElement = stack.Pop();
                var space = isLeft
                    ? new string(' ', topElement.Deep * 3)
                    : new string('-', topElement.Deep * 3);

                var connectionSign = GetConnectionSigh(topElement.ConnectionKind, isLeft);

                var line = isLeft
                    ? space + getNodeName(topElement) + connectionSign
                    : space + connectionSign + getNodeName(topElement);

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
        
        private void AddConnections(BindContoller<StringId> bindContoller, List<string> commands)
        {
            Contract.Requires(bindContoller != null);
            Contract.Requires(commands != null);

            foreach (var command in commands)
            {
                var partsOfCommand = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (partsOfCommand[0] == CommandMediator.AddLongName)
                {
                    bindContoller.Bind(new StringId(partsOfCommand[1]), new StringId(partsOfCommand[2]));
                }
                if (partsOfCommand[0] == CommandMediator.RemoveAllLongName)
                {
                    bindContoller.RemoveAllConnections();
                }
                if (partsOfCommand[0] == CommandMediator.RemoveLongName)
                {
                    bindContoller.RemoveConnection(new StringId(partsOfCommand[1]));
                }
            }
        }

        
    }
}