using System.Collections.Generic;
using BoundTree.Helpers.Actions;

namespace BoundTree.Helpers
{
    public class LogHistory
    {
        private readonly List<ILogResult> _logs;

        public LogHistory()
        {
            _logs = new List<ILogResult>();
        }

        public LogHistory(List<ILogResult> logs)
        {
            _logs = logs;
        }

        public void Add(ILogResult log)
        {
            _logs.Add(log);
        }

        public List<string> ToList()
        {
            var result = new List<string>();
            _logs.ForEach(log => result.AddRange(log.GetLines()));

            return result;
        }
    }
}