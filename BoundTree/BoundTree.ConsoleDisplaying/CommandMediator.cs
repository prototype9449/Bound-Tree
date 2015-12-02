using System;
using System.Diagnostics.Contracts;

namespace BoundTree.ConsoleDisplaying
{
    public class CommandMediator
    {
        public delegate void CommandHandler();

        public event CommandHandler Add;
        public event CommandHandler Remove;
        public event CommandHandler RemoveAll;
        public event CommandHandler Exit;

        public const string AddLongName = "add";
        public const string AddShortName = "a";

        public const string RemoveLongName = "remove";
        public const string RemoveShortName = "r";

        public const string RemoveAllLongName = "remove all";
        public const string RemoveAllShortName = "ra";

        public const string ExitLongName = "exit";
        public const string ExitShortName = "e";

        public void ProcessCommand(string command)
        {
            Contract.Requires(command != null);

            switch (command)
            {
                case AddShortName:
                case AddLongName:
                    OnCommand(Add);
                    break;

                case RemoveShortName:
                case RemoveLongName:
                    OnCommand(Remove);
                    break;

                case RemoveAllShortName:
                case RemoveAllLongName:
                    OnCommand(RemoveAll);
                    break;

                case ExitLongName:
                case ExitShortName:
                    OnCommand(Exit);
                    break;
            }
        }

        private void OnCommand(CommandHandler handler)
        {
            var localHandler = handler;
            if (localHandler != null) localHandler();
        }
    }
}