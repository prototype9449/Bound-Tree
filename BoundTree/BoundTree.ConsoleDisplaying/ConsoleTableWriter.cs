using System;
using System.Collections.Generic;
using System.Linq;
using BoundTree.Logic;

namespace BoundTree.ConsoleDisplaying
{
    public class Pair<T> where T : class, IEquatable<T>, new()
    {
        public DoubleNode<T> DoubleNode { get; set; } 

        public Pair(DoubleNode<T> doubleNode)
        {
            DoubleNode = doubleNode;
        }

        public bool IsVirtual { get; set; }

    }

    struct Table<T> where T :class, IEquatable<T>, new()
    {
        public Table(Pair<T> parents, IList<Pair<T>> childrens)
            : this()
        {
            Parents = parents;
            Childrens = childrens;
        }

        public Pair<T> Parents { get; set; }
        public IList<Pair<T>> Childrens { get; set; }
    }

    public class ConsoleTableWriter<T> where T : class, IEquatable<T>, new()
    {
        public void WriteToConsoleAsTables(DoubleNode<T> tree) 
        {
            var tables = CreateTables(tree);
            foreach (var table in tables.Where(table => table.Childrens.Count != 0))
            {
                WriteToConsole(table);
            }
        }

        private IList<Table<T>> CreateTables(DoubleNode<T> tree) 
        {
            var queue = new Queue<DoubleNode<T>>(new[] { tree });
            var tables = new HashSet<Table<T>>();
            while (queue.Count != 0)
            {
                var doubleNode = queue.Dequeue();
                var pairs = new List<Pair<T>>();
                foreach (DoubleNode<T> node in doubleNode.Nodes)
                {
                    pairs.Add(new Pair<T>(node));
                    queue.Enqueue(node);
                }
                var table = new Table<T>(new Pair<T>(doubleNode), pairs);
                if (!tables.Contains(table))
                {
                    tables.Add(table);
                }
            }

            return tables.ToList();
        }

        private string GetNodeName(Pair<T> pair, bool isLeft) 
        {
            return isLeft 
                ? pair.DoubleNode.MainLeaf.NodeInfo.GetType().Name + " " + pair.DoubleNode.MainLeaf.Id 
                : pair.DoubleNode.MinorLeaf.Id + " " + pair.DoubleNode.MinorLeaf.NodeInfo.GetType().Name;
        }

        private int GetMaxLength(Table<T> table, bool isLeft) 
        {
            return Math.Max(GetNodeName(table.Parents, isLeft).Length,
                table.Childrens.Max(pair => GetNodeName(pair, isLeft).Length));
        }

        private List<string> GetTableLines(Table<T> table, bool isLeft)
        {
            var maxLength = GetMaxLength(table, isLeft) + 5;
            var lines = new List<string>();
            var rootName = GetNodeName(table.Parents, isLeft);
            Func<Pair<T>, char> getSeparator = (pair) => pair.IsVirtual ? '-' : '=';
            Func<string, string, bool, string> getReplacedLine =
                (first, second, left) => left ? first + second : second + first;


            lines.Add(new string('*', maxLength));
            var firstLine = getReplacedLine(rootName,
                new String(getSeparator(table.Parents), maxLength - rootName.Length), isLeft);
            lines.Add(firstLine);
            lines.Add(new string('_', maxLength));
            lines.Add(new string(' ', maxLength));
            foreach (var pair in table.Childrens)
            {
                var nodeName = GetNodeName(pair, isLeft);
                var line = getReplacedLine(nodeName, new String(getSeparator(pair), maxLength - nodeName.Length), isLeft);
                lines.Add(line);
            }
            lines.Add(new string('*', maxLength));
            return lines;
        }

        private void WriteToConsole(Table<T> table)
        {
            var leftLines = GetTableLines(table, true);
            var rightLines = GetTableLines(table, false);
            for (int i = 0; i < leftLines.Count; i++)
            {
                Console.WriteLine(leftLines[i] + rightLines[i]);
            }
            Console.WriteLine();
        }

        
    }
}
