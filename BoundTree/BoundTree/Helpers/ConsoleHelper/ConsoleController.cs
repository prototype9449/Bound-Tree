using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.Logic;
using BoundTree.Logic.Nodes;
using BoundTree.Logic.Nodes.GeneralNodes;
using Single = BoundTree.Logic.Nodes.GeneralNodes.Single;

namespace BoundTree.Helpers.ConsoleHelper
{
    public class ConsoleController
    {
        private ConsoleConnectionController _consoleConnectionController;

        private SingleTree<StringId> _mainTree = new SingleTree<StringId>(null);
        private SingleTree<StringId> _minorTree = new SingleTree<StringId>(null);
        private List<string> _messages = new List<string>();
        

        public void Run()
        {
            ProcessBuildingTree(_mainTree);
            ProcessBuildingTree(_minorTree);
            _consoleConnectionController = new ConsoleConnectionController(new BindContoller<StringId>(_mainTree, _minorTree));
            _consoleConnectionController.Start();
        }

        private void ProcessBuildingTree(SingleTree<StringId> tree)
        {
            var factory = new SingleNodeFactory();

            SingleNode<StringId> root = factory.GetNode("Root", new Root());

            tree.Root = root;

            var ids = new HashSet<string>(new []{"Root"});

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
                    _messages.Add(String.Format("There is not such type of node like - {0}", secondCommand));
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

                if (parentNode.Node.CanContain(childNode.Node))
                {
                    _messages.Add(string.Format("The node {0} with type - {1} was added to {2} with type - {3}",
                        childNode.Node.Id, childTypeName, parentNode.Node.Id, parentTypeName));

                    parentNode.Add(childNode);
                    ids.Add(firstCommand);
                }
                else
                {
                    
                    _messages.Add(string.Format("The node with type {0} can not be added to the {1}",  childTypeName, parentTypeName));
                }
                
            }
        }

        private string GetNodeClassName(SingleNode<StringId> singleNode)
        {
            return singleNode.Node.NodeInfo.GetType().Name;
        }

        private void DisplayInitialCommand()
        {
            Console.Clear();
            new ConsoleTreeWriter<StringId>().WriteToConsoleAsTrees(_mainTree, _minorTree);
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
