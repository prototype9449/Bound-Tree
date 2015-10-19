using System;
using System.Collections.Generic;

namespace BoundTree.Helpers
{
    public class ConsoleController
    {
        private readonly BindContoller<string> _bindController;

        public ConsoleController(BindContoller<string> bindController)
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
                KeyValuePair<string, string> ids = new KeyValuePair<string, string>();
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
            var tree = new TreeFiller<string>().GetFilledTree(_bindController.MainTree, _bindController.MinorTree);
            new ConsoleTreeWriter<string>().WriteToConsoleAsTrees(_bindController.MainTree, _bindController.MinorTree);
            new ConsoleTreeWriter<string>().WriteToConsoleAsTrees(tree);
        }

        private KeyValuePair<string,string> GetIds(bool once)
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
            
            return new KeyValuePair<string, string>(main, minor);
        }
    }
}