using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Helpers;
using BoundTree.Logic;
using BoundTree.Logic.LogicLevelProviders;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;
using BoundTree.TreeReconstruction;

namespace BoundTree.ConsoleDisplaying
{
    public class ConsoleConnectionController
    {
        private readonly NodeInfoFactory _nodeInfoFactory;
        private BindContoller<StringId> _bindController;
        private DoubleNode<StringId> _currentDoubleNode;

        private readonly TreeLogger _treeLogger;
        private readonly List<string> _messages = new List<string>();
        private readonly CommandMediator _commandMediator = new CommandMediator();
        private readonly ConsoleTreeWriter _consoleTreeWriter = new ConsoleTreeWriter();
        private readonly TreeConverter<StringId> _treeConverter = new TreeConverter<StringId>();
        private readonly TreeConstructor<StringId> _treeConstructor;

        public ConsoleConnectionController(TreeConstructor<StringId> treeConstructor, NodeInfoFactory nodeInfoFactory)
        {
            _nodeInfoFactory = nodeInfoFactory;
            _treeConstructor = treeConstructor;
            _treeLogger = TreeLogger.GetTreeLogger();
            Subscribe();
        }

        public MultiTree<StringId> GetConnectedMultiTree()
        {
            Contract.Requires(_bindController.MainMultiTree != null);

            return new MultiTree<StringId>(_currentDoubleNode.ToMultiNode());
        }

        private void Subscribe()
        {
            _commandMediator.Remove += RemoveConnection;
            _commandMediator.RemoveAll += RemoveAllConnections;
            _commandMediator.Add += AddConnection;
        }

        private void AddConnection()
        {
            var ids = GetIds(false);
            if (_bindController.Bind(ids.Key, ids.Value))
            {
                _treeLogger.ProcessCommand(string.Format("{0} {1} {2}", CommandMediator.AddLongName, ids.Key, ids.Value));
                _messages.Add(string.Format("The {0} have been connected with {1}", ids.Key, ids.Value));
                return;
            }
            _messages.Add(string.Format("The {0} have not been connected with {1}", ids.Key, ids.Value));
        }

        private void RemoveConnection()
        {
            var ids = GetIds(true);
            if (_bindController.RemoveConnection(ids.Key))
            {
                _treeLogger.ProcessCommand(string.Format("{0} {1}", CommandMediator.RemoveLongName, ids.Key));
                _messages.Add(string.Format("The connection with {0} was removed", ids.Key));
                return;
            }
            _messages.Add(string.Format("The {0} was not removed", ids.Key));
        }

        private void RemoveAllConnections()
        {
            if (_bindController.RemoveAllConnections())
            {
                _treeLogger.ProcessCommand(CommandMediator.RemoveAllLongName);
                _messages.Add("The all connections were removed");
                return;
            }
            _messages.Add("The all connections were not removed");
        }

        public void Start(BindContoller<StringId> bindContoller)
        {
            Contract.Requires(bindContoller != null);
            _bindController = bindContoller;

            while (true)
            {
                DisplayTree();
                Console.WriteLine("Type 'a' to add, 'r' to remove, 'ra' to remove all connection, 'e' to exit");
                var action = Console.ReadLine();
                _commandMediator.ProcessCommand(action);
                if (action == CommandMediator.ExitLongName || action == CommandMediator.ExitShortName)
                    break;
            }
        }

        private void DisplayTree()
        {
            Console.Clear();

            var idGenerator = new IdGenerator(_bindController.MainMultiTree.ToList());
            _currentDoubleNode = _treeConstructor.GetFilledTree(_bindController, idGenerator);

            Console.WriteLine(_consoleTreeWriter.ConvertToString(_bindController.MainMultiTree, _bindController.MinorSingleTree));
            Console.WriteLine(_consoleTreeWriter.ConvertToString(_currentDoubleNode));

            _treeConverter.ConvertMultiTreeAsMulti(new MultiTree<StringId>(_currentDoubleNode.ToMultiNode())).ForEach(Console.WriteLine);

            Console.WriteLine();
            if (_messages.Any())
            {
                Console.WriteLine(_messages.Last());
            }
            Console.WriteLine();
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