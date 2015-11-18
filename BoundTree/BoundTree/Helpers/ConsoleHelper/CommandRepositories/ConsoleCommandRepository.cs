using System;
using System.Collections;
using System.Collections.Generic;

namespace BoundTree.Helpers.ConsoleHelper.CommandRepositories
{
    public class ConsoleCommandRepository : IEnumerator<string>
    {
        public void Dispose() { }

        public bool MoveNext()
        {
            return true;
        }

        public void Reset() { }

        public string Current
        {
            get
            {
                return Console.ReadLine();
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}