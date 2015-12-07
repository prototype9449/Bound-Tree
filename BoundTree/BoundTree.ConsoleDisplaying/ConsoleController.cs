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
        private SingleTree<StringId> _minorTree;
        private readonly ConsoleConnectionController _consoleConnectionController = new ConsoleConnectionController();
        private readonly List<string> _messages = new List<string>();
        private readonly ConsoleTreeWriter _consoleTreeWriter = new ConsoleTreeWriter();
        private readonly SingleNodeFactory factory = new SingleNodeFactory();

        public ConsoleController()
        {
            _mainSingleTree = new SingleTree<StringId>(factory.GetNode("Root", new Root()));
            _minorTree = new SingleTree<StringId>(factory.GetNode("Root", new Root()));
        }

        public void Run()
        {
            Console.WriteLine("Do you want to open existed file?");
            Console.WriteLine("Type 'y' if you want to open file");
            var answer = Console.ReadLine();
            var action = SelectFile(answer);
            if (!action)
            {
                ProcessBuildingMainTree();
            }

            do
            {
                ProcessBuildingMinorTree();
                
                _consoleConnectionController.Start(new BindContoller<StringId>(_mainMultiTree, _minorTree));

                _mainMultiTree = _consoleConnectionController.GetConnectedMultiTree();
                _minorTree = new SingleTree<StringId>(factory.GetNode("Root", new Root()));

                Console.Clear();
                Console.WriteLine("Do you want to type another tree? \n type 'yes' to add tree or something else to finish with building trees");
                if (Console.ReadLine() != "yes")
                {
                    return;
                }

            } while (true);
        }

       

        private bool SelectFile(string answer)
        {
            if (answer == "y")
            {
                var dialog = new OpenFileDialog();
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var lines = File.ReadAllLines(dialog.FileName).ToList();
                    var treeData = new DoubleNodeParser().GetDoubleNode(lines);
                    _mainMultiTree = new MultiTree<StringId>(treeData.MainSingleTree);
                    _minorTree = treeData.MinorSingleTree;
                    return true;
                }
            }

            return false;
        }

        private void ProcessBuildingMainTree()
        {
            _mainSingleTree = new SingleTree<StringId>(factory.GetNode("Root", new Root()));
            ProcessBuildingTree(_mainSingleTree);
            _mainMultiTree = new MultiTree<StringId>(_mainSingleTree);
        }

        private void ProcessBuildingMinorTree()
        {
            ProcessBuildingTree(_minorTree);
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
                var childNode = factory.GetNode(firstCommand, NodeInfoFactory.GetNodeInfo(secondCommand));

                var parentTypeName = GetNodeClassName(parentNode);
                var childTypeName = GetNodeClassName(childNode);

                if (parentNode.SingleNodeData.CanContain(childNode.SingleNodeData))
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
            if (!ReferenceEquals(_mainMultiTree, null))
            {
                Console.WriteLine(_consoleTreeWriter.ConvertToString(_mainMultiTree, _minorTree));
            }
            else
            {
                Console.WriteLine(_consoleTreeWriter.ConvertToString(_mainSingleTree, _minorTree));
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
