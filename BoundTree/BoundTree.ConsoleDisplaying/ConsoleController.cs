using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BoundTree.Helpers;
using BoundTree.Logic;
using BoundTree.Logic.Nodes;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;

namespace BoundTree.ConsoleDisplaying
{
    public class ConsoleController
    {
        private SingleTree<StringId> _mainSingleTree;
        private MultiTree<StringId> _mainMultiTree;
        private SingleTree<StringId> _minorSingleTree;

        private readonly ConsoleConnectionController _consoleConnectionController = new ConsoleConnectionController();
        private readonly List<string> _messages = new List<string>();
        private readonly ConsoleTreeWriter _consoleTreeWriter = new ConsoleTreeWriter();
        private readonly SingleNodeFactory _factory = new SingleNodeFactory();
        private readonly TreeLogger _treeLogger = TreeLogger.GetTreeLogger();
        private readonly MultiTreeParser _multiTreeParser = new MultiTreeParser();
        private readonly SingleTreeParser _singleTreeParser = new SingleTreeParser();

        public ConsoleController()
        {
            _mainSingleTree = new SingleTree<StringId>(_factory.GetNode("Root", new Root()));
            _minorSingleTree = new SingleTree<StringId>(_factory.GetNode("Root", new Root()));
        }

        public void Run()
        {
            Console.WriteLine("Do you want to open existed file?");
            Console.WriteLine("Type 'y' if you want to open file");
            if (Console.ReadLine() == "y")
            {
                ProcessBuildingTreeFromFile();
            }
            else
            {
                ProcessBuildingMainTree();
            }
        }

        private void ProcessBuildingTreeFromConsole(BindContoller<StringId> bindController = null)
        {
            BindContoller<StringId> newBindController = bindController;

            do
            {
                if (newBindController == null)
                {
                    ProcessBuildingMinorTree();

                    _consoleConnectionController.Start(new BindContoller<StringId>(_mainMultiTree, _minorSingleTree));
                }
                else
                {
                    _consoleConnectionController.Start(newBindController);
                }

                _mainMultiTree = _consoleConnectionController.GetConnectedMultiTree();

                Console.Clear();
                Console.WriteLine("Do you want to type another tree?");
                Console.WriteLine("type 'yes' to add tree or something else to finish with building trees");
                if (Console.ReadLine() != "yes")
                {
                    break;
                }
                newBindController = null;

            } while (true);
            _treeLogger.AddMultiTreeInFile(_mainMultiTree);
        }

        private void ProcessBuildingTreeFromFile()
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var lines = File.ReadAllLines(dialog.FileName).ToList();
                lines.Reverse();
                var restOfLinesCount = lines.SkipWhile(line => line != "");
                var resultLines = restOfLinesCount.Reverse().ToList();

                var bindControllerAndLogHistory = _multiTreeParser.GetBindContollerAndLogHistory(resultLines);
                _treeLogger.AddLogHistory(bindControllerAndLogHistory.Second);

                ProcessBuildingTreeFromConsole(bindControllerAndLogHistory.First);

                return;
            }

            throw new InvalidOperationException("DialogResult is not OK");
        }

        private void ProcessBuildingMainTree()
        {
            _mainSingleTree = new SingleTree<StringId>(_factory.GetNode("Root", new Root()));
            ProcessBuildingTree(_mainSingleTree);
            _treeLogger.AddSinlgeTreeInFile(_mainSingleTree);
            _mainMultiTree = new MultiTree<StringId>(_mainSingleTree);
            ProcessBuildingTreeFromConsole();
        }

        private void ProcessBuildingMinorTree()
        {
            _minorSingleTree = new SingleTree<StringId>(_factory.GetNode("Root", new Root()));
            ProcessBuildingTree(_minorSingleTree);
            _treeLogger.AddSinlgeTreeInFile(_minorSingleTree);
        }

        private void ProcessBuildingTree(SingleTree<StringId> tree)
        {
            Contract.Requires(tree != null);

            var ids = new HashSet<string>(new[] { "Root" });

            while (true)
            {
                DisplayInitialCommand();

                var inputLine = Console.ReadLine();
                var commands = inputLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (commands.Length != 3 && commands.Length != 1)
                {
                    _messages.Add("Not full set of commands");
                    continue;
                }

                var firstCommand = commands[0];

                if (commands.Length == 1)
                {
                    if (firstCommand == "end")
                    {
                        _messages.Add("Tree was successfully built");
                        break;
                    }

                    _messages.Add("There is not such command");
                    continue;
                }

                if (ids.Contains(firstCommand))
                {
                    _messages.Add("The such id already exists");
                    DisplayInitialCommand();
                    continue;
                }

                var secondCommand = commands[1];

                if (!NodeInfoFactory.Contains(secondCommand))
                {
                    _messages.Add(String.Format("There is not such type of NodeData like - {0}", secondCommand));
                    DisplayInitialCommand();
                    continue;
                }

                var thirdCommand = commands[2];

                if (!ids.Contains(thirdCommand))
                {
                    _messages.Add(String.Format("The such parent id like {0} does not exist", thirdCommand));
                    DisplayInitialCommand();
                    continue;
                }

                var parentNode = tree.GetById(new StringId(thirdCommand));
                var childNode = _factory.GetNode(firstCommand, NodeInfoFactory.GetNodeInfo(secondCommand));

                var parentTypeName = GetNodeClassName(parentNode);
                var childTypeName = GetNodeClassName(childNode);

                if (parentNode.CanContain(childNode))
                {
                    _messages.Add(string.Format("The Node {0} with type - {1} was added to {2} with type - {3}",
                        childNode.SingleNodeData.Id, childTypeName, parentNode.SingleNodeData.Id, parentTypeName));

                    parentNode.Add(childNode);
                    ids.Add(firstCommand);
                }
                else
                {
                    _messages.Add(string.Format("The NodeData with type {0} can not be added to the {1}", childTypeName, parentTypeName));
                }
            }
        }

        private string GetNodeClassName(SingleNode<StringId> singleNode)
        {
            Contract.Requires(singleNode != null);

            return singleNode.NodeType.Name;
        }

        private void DisplayInitialCommand()
        {
            Console.Clear();
            if (ReferenceEquals(_mainMultiTree, null))
            {
                Console.WriteLine(_consoleTreeWriter.ConvertToString(_mainSingleTree, _minorSingleTree));
            }
            else
            {
                Console.WriteLine(_consoleTreeWriter.ConvertToString(_mainMultiTree, _minorSingleTree));
            }
            Console.WriteLine();
            if (_messages.Any())
            {
                Console.WriteLine(_messages.Last());
            }
            Console.WriteLine();
            Console.WriteLine("Enter 'end' to finish with building of current tree");
            Console.WriteLine("Enter id, type and id of parent");
        }
    }
}
