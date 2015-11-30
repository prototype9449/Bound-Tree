using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BoundTree.Helpers;
using Build.TestFramework;

namespace TreeConverterToNewFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Bound-Tree\BoundTree\IntegrationTestsOfTrees\Tests";
            var files = Directory.GetFiles(path);
            foreach (var fileName in files)
            {
                var lines = File.ReadAllLines(fileName).ToList();
                var doubleNode = new DoubleNodeParser().GetDoubleNode(lines);
                var outputLines = new DoubleNodeConverter().ConvertDoubleNode(doubleNode);
                File.Delete(fileName);
                File.Create(fileName).Dispose();

                List<string> beforeLines = null;
                var lastNotEmptyIndex = lines.FindLastIndex(line => line != "");
                for (int i = lastNotEmptyIndex; i >= 0; i--)
                {
                    if (lines[i] == "")
                    {
                        beforeLines = lines.Take(i).ToList();
                        beforeLines.Add(Environment.NewLine);
                        break;
                    }

                }

                if (beforeLines == null)
                {
                    throw new FileLoadException();
                }

                File.AppendAllLines(fileName, beforeLines);
                File.AppendAllLines(fileName, outputLines);
            }
        }
    }
}
