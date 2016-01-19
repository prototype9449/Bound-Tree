using System.Collections.Generic;

namespace BoundTree.Helpers.Actions
{
    public class CommandsResult : ILogResult
    {
        private readonly List<string> _commnads;

        public CommandsResult(List<string> commnads)
        {
            _commnads = commnads;
        }

        public List<string> GetLines()
        {
            return _commnads;
        }
    }
}