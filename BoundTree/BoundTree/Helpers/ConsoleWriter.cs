using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundTree.Helpers
{
    struct Pair
    {
        public Pair(Node firstNode, Node secondNode) : this()
        {
            FirstNode = firstNode;
            SecondNode = secondNode;
        }

        public Node FirstNode { get; set; }
        public Node SecondNode { get; set; }
    }

    struct Table
    {
        public Table(Pair parents, IList<Pair> childrens) : this()
        {
            Parents = parents;
            Childrens = childrens;
        }

        public Pair Parents { get; set; }
        public IList<Pair> Childrens { get; set; }
    }

    public class ConsoleWriter
    {
        private IList<Table> CreateTables(Tree firstTree, Tree secondTree)
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
                    pairs.Add(new Pair(firstNode.Nodes[i], secondNode.Nodes[i]));

                    firstQueue.Enqueue(firstNode.Nodes[i]);
                    secondQueue.Enqueue(secondNode.Nodes[i]);
                }
                var table = new Table(new Pair(firstNode, secondNode), pairs);
                if (!tables.Contains(table))
                {
                    tables.Add(table);
                }
            }

            return tables.ToList();
        }

        private void WriteToConsole(Table table)
        {
            Console.WriteLine(new string('*', 10));
            Console.WriteLine("{0} --- {1}", table.Parents.FirstNode.Id, table.Parents.SecondNode.Id);
            Console.WriteLine(new string('-',10));
            foreach (var pair in table.Childrens)
            {
                Console.WriteLine("{0} --- {1}", pair.FirstNode.Id, pair.SecondNode.Id);
            }
            Console.WriteLine(new string('*', 10));
            Console.WriteLine();
        }

        public void WriteToConsole(Tree firstTree, Tree secondTree)
        {
            var tables = CreateTables(firstTree, secondTree);
            foreach (var table in tables.Where(table => table.Childrens.Count != 0))
            {
                WriteToConsole(table);
            }
        }


    }
}
