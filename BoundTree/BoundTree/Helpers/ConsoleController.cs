using System;
using System.Collections.Generic;
using BoundTree.Logic;

namespace BoundTree.Helpers
{
    public class ConsoleController
    {
        private readonly BindContoller<StringId> _bindController;

        public ConsoleController(BindContoller<StringId> bindController)
        {
            _bindController = bindController;
        }

        public void Start()
        {
            while (true)
            {
                DisplayTree();
                Console.WriteLine("Type 'a' to Add, 'r' to Remove, 'e' to exit");
                var action = Console.ReadLine();
                var ids = new KeyValuePair<StringId, StringId>();
                switch (action)
                {
                    case "r" :
                        ids = GetIds(true);
                        _bindController.RemoveConnection(ids.Key);
                        break;
                    case "a" : 
                      ids = GetIds(false);
                        _bindController.Bind(ids.Key, ids.Value);
                        break;
                }
               
                if (action == "e")
                {
                    break;
                }
            }
        }

        private void DisplayTree()
        {
            Console.Clear();
            var tree = new TreeFiller<StringId>(_bindController).GetFilledTree();
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