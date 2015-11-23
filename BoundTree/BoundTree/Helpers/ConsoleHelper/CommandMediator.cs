using System;

namespace BoundTree.Helpers.ConsoleHelper
{
    public class CommandMediator
    {
        public event EventHandler Add;
        public event EventHandler Remove;
        public event EventHandler RemoveAll;
        public event EventHandler Exit;

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
            switch (command)
            {
                case AddShortName:
                case AddLongName:
                    OnAddCommand();
                    break;

                case RemoveShortName:
                case RemoveLongName:
                    OnRemoveCommand();
                    break;

                case RemoveAllShortName:
                case RemoveAllLongName:
                    OnRemoveAllCommand();
                    break;

                case ExitLongName:
                case ExitShortName:
                    OnExit();
                    break;
            }
        }

        protected virtual void OnAddCommand()
        {
            var handler = Add;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnRemoveCommand()
        {
            var handler = Remove;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnRemoveAllCommand()
        {
            var handler = RemoveAll;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnExit()
        {
            var handler = Exit;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}