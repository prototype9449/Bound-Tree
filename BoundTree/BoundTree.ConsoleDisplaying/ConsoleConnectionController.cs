using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BoundTree.Helpers;
using BoundTree.Logic;
using BoundTree.TreeReconstruction;

namespace BoundTree.ConsoleDisplaying
{
    public class ConsoleConnectionController
    {
        private readonly BindContoller<StringId> _bindController;
        private readonly TreeLogger _treeLogger;
        private readonly List<string> _messages = new List<string>();
        private readonly CommandMediator _commandMediator = new CommandMediator();
        private readonly ConsoleTreeWriter _treeWriter = new ConsoleTreeWriter();

        private DoubleNode<StringId> _previousDoubleNode;
        private DoubleNode<StringId> _currentDoubleNode;

        public ConsoleConnectionController(BindContoller<StringId> bindController)
        {
            Contract.Requires(bindController !=null);

            _bindController = bindController;
            _treeLogger = new TreeLogger(bindController.MainSingleTree, bindController.MinorSingleTree);
            Subscribe();
        }

        private void Subscribe()
        {
            _commandMediator.Remove += RemoveConnection;
            _commandMediator.RemoveAll += RemoveAllConnections;
            _commandMediator.Add += AddConnection;
            _commandMediator.Exit += ExitFromBuildingTree;
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

        private void ExitFromBuildingTree()
        {
            _treeLogger.AddDoubleTreeToFile(_currentDoubleNode);
        }

        public void Start()
        {
            while (true)
            {
                DisplayTree();
                Console.WriteLine("Type 'a' to add, 'r' to remove, 'ra' to remove all connection, 'e' to exit");
                var action = Console.ReadLine();
                _commandMediator.ProcessCommand(action);
                if(action == CommandMediator.ExitLongName || action == CommandMediator.ExitShortName)
                    break;
            }
        }

        private void DisplayTree()
        {
            Console.Clear();
            try
            {
                _currentDoubleNode = new TreeReconstruction<StringId>(_bindController).GetFilledTree();
                _previousDoubleNode = _currentDoubleNode;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine(e.StackTrace);
                Console.WriteLine();
                Console.WriteLine("Sorry, there was an error");
                Console.WriteLine("Press any button to continue");
                _currentDoubleNode= _previousDoubleNode;
                Console.ReadKey();
            }

            Console.WriteLine(_treeWriter.ConvertToString(_bindController.MainSingleTree, _bindController.MinorSingleTree));
            Console.WriteLine(_treeWriter.ConvertToString(_currentDoubleNode));

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