using System;
using System.Collections.Generic;
using System.Threading;
using BoundTree.Helpers.TreeReconstruction;
using BoundTree.Logic;

namespace BoundTree.Helpers.ConsoleHelper
{
    public class ConsoleConnectionController
    {
        private readonly BindContoller<StringId> _bindController;
        private DoubleNode<StringId> _preivousDoubleNode;
        private TreeLogger _treeLogger;

        private List<string> _messages = new List<string>();

        public ConsoleConnectionController(BindContoller<StringId> bindController)
        {
            _bindController = bindController;
            _treeLogger = new TreeLogger(bindController.MainSingleTree, bindController.MinorSingleTree);
        }

        public void Start()
        {
            while (true)
            {
                DisplayTree();
                Console.WriteLine("Type 'a' to add, 'r' to remove, 'ra' to remove all connection, 'e' to exit");
                var action = Console.ReadLine();
                var ids = new KeyValuePair<StringId, StringId>();

                switch (action)
                {
                    case "r":
                        ids = GetIds(true);
                        if (_bindController.RemoveConnection(ids.Key))
                        {
                            _treeLogger.ProcessCommand(string.Format("remove {0}", ids.Key));
                        }
                        break;
                    case "a":
                        ids = GetIds(false);
                        if (_bindController.Bind(ids.Key, ids.Value))
                        {
                            _treeLogger.ProcessCommand(string.Format("add {0} {1}", ids.Key, ids.Value));
                        }
                        break;
                    case "ra":
                        if (_bindController.ClearConnection())
                        {
                            _treeLogger.ProcessCommand("remove all");
                        }
                        break;
                }

                if (action == "e")
                {
                    _treeLogger.ProcessCommand("exit");
                    break;
                }
            }
        }

        private void DisplayTree()
        {
            Console.Clear();
            DoubleNode<StringId> tree;
            try
            {
                tree = new TreeReconstruction<StringId>(_bindController).GetFilledTree();
                _preivousDoubleNode = tree;
            }
            catch (Exception)
            {
                Console.WriteLine("Sorry, there was an error");
                Console.WriteLine("Press any button to continue");
                tree = _preivousDoubleNode;
                Console.ReadKey();
            }
            new ConsoleTreeWriter<StringId>().WriteToConsoleAsTrees(_bindController.MainSingleTree, _bindController.MinorSingleTree);
            new ConsoleTreeWriter<StringId>().WriteToConsoleAsTrees(tree);
        }

        private KeyValuePair<StringId, StringId> GetIds(bool once)
        {
            var main = "";
            var minor = "";
            if (once)
            {
                Console.Write("Main :");
                main = Console.ReadLine();
            }
            else
            {
                Console.Write("Main :");
                main = Console.ReadLine();
                Console.Write("Minor :");
                minor = Console.ReadLine();
            }

            return new KeyValuePair<StringId, StringId>(new StringId(main), new StringId(minor));
        }
    }
}