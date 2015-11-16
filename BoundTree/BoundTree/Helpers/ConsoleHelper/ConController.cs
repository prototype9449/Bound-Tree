﻿using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.Logic;
using BoundTree.Logic.Nodes;
using Single = BoundTree.Logic.Nodes.Single;

namespace BoundTree.Helpers.ConsoleHelper
{
    public class ConController
    {
        private readonly BindContoller<StringId> _bindController;
        private SingleTree<StringId> _mainTree = new SingleTree<StringId>(null);
        private SingleTree<StringId> _minorTree = new SingleTree<StringId>(null);
        private static Dictionary<string, NodeInfo> _nodeTypes = new Dictionary<string, NodeInfo>();

        static ConController()
        {
            _nodeTypes.Add("Single", new Single());
            _nodeTypes.Add("Grid", new Grid());
            _nodeTypes.Add("Multi", new Multi());
            _nodeTypes.Add("MultiGrid", new MultiGrid());
            _nodeTypes.Add("OpenText", new OpenTextInfo());
            _nodeTypes.Add("Grid3D", new Grid3D());
            _nodeTypes.Add("Answer", new AnswerInfo());
            _nodeTypes.Add("PredefinedList", new PredefinedList());
        }

        public void Run()
        {
            ProcessTree(_mainTree);
            ProcessTree(_minorTree);
        }

        private void ProcessTree(SingleTree<StringId> tree)
        {
            var fabrica = new SingleNodeFabrica();

            SingleNode<StringId> root = fabrica.GetNode("Root", new Root());

            tree.Root = root;

            var ids = new HashSet<string>();

            while (true)
            {
                DisplayInitialCommand();

                var inputLine = Console.ReadLine();
                var commands = inputLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (commands.Length != 3 && commands.Length != 1)
                {
                    Console.WriteLine("Not full line of commands");
                    continue;
                }
                
                if (commands.Length == 1)
                {
                    if (commands[0] == "end")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Ok - the first tree was built");
                        break;
                    }

                    Console.WriteLine("There is not such command");
                    continue;
                }


                if (ids.Contains(commands[0]))
                {
                    Console.WriteLine("The such id is already existed");
                    DisplayInitialCommand();
                    continue;
                }

                if (!_nodeTypes.ContainsKey(commands[1]))
                {
                    Console.WriteLine("There is not such type of node");
                    DisplayInitialCommand();
                    continue;
                }

                if (ids.Contains(commands[2]))
                {
                    Console.WriteLine("The such parent id is not existed");
                    DisplayInitialCommand();
                    continue;
                }

                var parentNode = tree.GetById(new StringId(commands[2]));
                parentNode.Add(fabrica.GetNode(commands[0], _nodeTypes[commands[1]]));
            }
        }

        private void DisplayInitialCommand()
        {
            Console.Clear();
            new ConsoleTreeWriter<StringId>().WriteToConsoleAsTrees(_mainTree, _minorTree);
            Console.WriteLine("Enter id and type");
        }
    }
}
