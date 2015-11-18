using System.Collections;
using System.Collections.Generic;

namespace BoundTree.Helpers.ConsoleHelper.CommandRepositories
{
    public class MemoryCommandRepository : IEnumerator<string>
    {
        private readonly List<string> _commands;
        private int _currentIndex = 0;
        
        public MemoryCommandRepository(List<string> commands)
        {
            _commands = commands;
        }

        public void Dispose()
        {
            _currentIndex = 0;
        }

        public bool MoveNext()
        {
            return _currentIndex < _commands.Count;
        }

        public void Reset()
        {
            _currentIndex = 0;
        }

        public string Current
        {
            get { return _commands[_currentIndex]; } 
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}