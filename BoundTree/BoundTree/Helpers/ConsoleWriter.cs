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
        public Pair(Node firstNode, Node secondNode, bool isVirtual) : this()
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
        public Table(Pair parents, IList<Pair> childrens) : this()
        {
            Parents = parents;
            Childrens = childrens;
        }

        public Pair Parents { get; set; }
        public IList<Pair> Childrens { get; set; }
    }

    struct TreeNode
    {
        public Node Node { get; set; }
        public int Deep { get; set; }

        public TreeNode(Node node, int deep) : this()
        {
            Node = node;
            Deep = deep;
        }
    }

    public class ConsoleWriter
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

        private void WriteToConsole(Table table)
        {
            Console.WriteLine(new string('*', 50));
            var getSeparator = new Func<Pair, string>(pair => pair.IsVirtual ? "----" : "<-->");
            var getNodeName = new Func<Node, string>(node => node.Id + " " + node.NodeInfo.GetType().Name);

            Console.WriteLine("{0} {1} {2}", getNodeName(table.Parents.FirstNode), getSeparator(table.Parents), getNodeName(table.Parents.SecondNode));
            Console.WriteLine(new string('-',50));
            foreach (var pair in table.Childrens)
            {
                Console.WriteLine("{0} {1} {2}", getNodeName(pair.FirstNode), getSeparator(pair), getNodeName(pair.SecondNode));
            }
            Console.WriteLine(new string('*', 50));
            Console.WriteLine();
        }
        
        public void WriteToConsoleAsTrees(Tree firstTree, Tree secondTree, BindingHandler bindingHandler)
        {
            var ids = bindingHandler.BoundNodes.Select(pair => pair.Key).ToList();

            var firstTreeLines = GetNodeLines(firstTree,ids, true);
            var secondTreeLines = GetNodeLines(secondTree, ids, false);

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < firstTreeLines.Count; i++)
            {
                stringBuilder.AppendLine(firstTreeLines[i] + '-' + secondTreeLines[i]);
                stringBuilder.AppendLine();
            }
            Console.WriteLine(stringBuilder);
        }

        private List<string> GetNodeLines(Tree tree, List<int> ids, bool isLeft)
        {
            var nodeLines = new List<string>();
            var stack = new Stack<Node>();
            stack.Push(tree.Root);

            Func<Node, string> getNodeName = (node) => isLeft ? node.NodeInfo.GetType().Name + '(' + node.Id + ')' : "(" + node.Id + ')' + node.NodeInfo.GetType().Name;
            
            while (stack.Count != 0)
            {
                var topElement = stack.Pop();

                var maxDeep = tree.ToList().Max(node => node.Deep);

                var space = isLeft ? new string(' ', topElement.Deep * 3) : new String('-', (maxDeep - topElement.Deep)*3);

                var additionalSeparator = ids.Contains(topElement.Id) ? isLeft ? '<' : '>' : '-';
                var line = isLeft
                    ? space + getNodeName(topElement) + additionalSeparator
                    : space + additionalSeparator + getNodeName(topElement);
                
                nodeLines.Add(line);
                 
                foreach (var node in topElement.Nodes.OrderByDescending(node => node.Id))
                {
                    stack.Push(node);
                }
            }

            var maxLength = nodeLines.Max(line => line.Length);

            Func<string, string, bool, string> swapPlaces = (first, second, isItLeft) => isItLeft ? first + second : second + first;

            nodeLines =
                nodeLines.Select(
                    line =>
                        isLeft
                            ? line += new String('-', maxLength - line.Length)
                            : line += new String(' ', maxLength - line.Length)).ToList();
            var lengths = nodeLines.Select(x => x.Length).ToList();
            return nodeLines;
        }
    }
}
