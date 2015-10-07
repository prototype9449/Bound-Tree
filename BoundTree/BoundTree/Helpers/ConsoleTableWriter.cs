using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace BoundTree.Helpers
{
    struct Pair
    {
        public Pair(Node firstNode, Node secondNode, bool isVirtual)
            : this()
        {
            FirstNode = firstNode;
            SecondNode = secondNode;
            IsVirtual = isVirtual;
        }

        public bool IsVirtual { get; set; }
        public Node FirstNode { get; set; }
        public Node SecondNode { get; set; }
    }

    struct Table
    {
        public Table(Pair parents, IList<Pair> childrens)
            : this()
        {
            Parents = parents;
            Childrens = childrens;
        }

        public Pair Parents { get; set; }
        public IList<Pair> Childrens { get; set; }
    }

    public class ConsoleTableWriter
    {
        public void WriteToConsoleAsTables(Tree firstTree, Tree secondTree, BindingHandler bindingHandler)
        {
            var ids = bindingHandler.BoundNodes.Select(pair => pair.Key).ToList();
            var tables = CreateTables(firstTree, secondTree, ids);
            foreach (var table in tables.Where(table => table.Childrens.Count != 0))
            {
                WriteToConsole(table);
            }
        }

        private IList<Table> CreateTables(Tree firstTree, Tree secondTree, IList<int> ids)
        {
            var firstQueue = new Queue<Node>(new[] { firstTree.Root });
            var secondQueue = new Queue<Node>(new[] { secondTree.Root });
            var tables = new HashSet<Table>();
            while (firstQueue.Count != 0)
            {
                var firstNode = firstQueue.Dequeue();
                var secondNode = secondQueue.Dequeue();
                var pairs = new List<Pair>();
                for (var i = 0; i < firstNode.Nodes.Count; i++)
                {
                    pairs.Add(new Pair(firstNode.Nodes[i], secondNode.Nodes[i], !ids.Contains(firstNode.Nodes[i].Id)));

                    firstQueue.Enqueue(firstNode.Nodes[i]);
                    secondQueue.Enqueue(secondNode.Nodes[i]);
                }
                var table = new Table(new Pair(firstNode, secondNode, !ids.Contains(firstNode.Id)), pairs);
                if (!tables.Contains(table))
                {
                    tables.Add(table);
                }
            }

            return tables.ToList();
        }

        private string GetNodeName(Pair pair, bool isLeft)
        {
            return isLeft ? pair.FirstNode.NodeInfo.GetType().Name + " " + pair.FirstNode.Id : pair.SecondNode.Id +  " " + pair.SecondNode.NodeInfo.GetType().Name;
        }

        private int GetMaxLength(Table table, bool isLeft)
        {
            return Math.Max(GetNodeName(table.Parents, isLeft).Length,
                table.Childrens.Max(pair => GetNodeName(pair, isLeft).Length));
        }

        private List<string> GetTableLines(Table table, bool isLeft)
        {
            var maxLength = GetMaxLength(table, isLeft) + 5;
            var lines = new List<string>();
            var rootName = GetNodeName(table.Parents, isLeft);
            Func<Pair, char> getSeparator = (pair) => pair.IsVirtual ? '-' : '=';
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

        private void WriteToConsole(Table table)
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
