﻿using System;
using BoundTree;
using BoundTree.Helpers;
using BoundTree.Logic;
using BoundTree.NodeInfo;

namespace ConsoleAppForTesting
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bindingHandler = new BindingHandler<StringId>();
            var mainTree = GetMainTree(bindingHandler);
            var minorTree = GetMinorTree(bindingHandler);

            var bindController = new BindContoller<StringId>(mainTree, minorTree);

            new ConsoleController(bindController).Start();
        }

        public static SingleTree<StringId> GetMainTree(BindingHandler<StringId> bindingHandler)
        {
            var nodeInfoFabrica = new NodeInfoFabrica();
            var singleNodeFabrica = new SingleNodeFabrica();

            var tree = singleNodeFabrica.GetNode("A", nodeInfoFabrica.Root, new[]
            {
                 singleNodeFabrica.GetNode("B", nodeInfoFabrica.SingleQustion),
                 singleNodeFabrica.GetNode("C", nodeInfoFabrica.GridQuestion, new[]
                {
                     singleNodeFabrica.GetNode("D", nodeInfoFabrica.OpenTextInfo),
                     singleNodeFabrica.GetNode("E", nodeInfoFabrica.SingleQustion)
                }),
                 singleNodeFabrica.GetNode("F", nodeInfoFabrica.GridQuestion, new[]
                {
                     singleNodeFabrica.GetNode("G", nodeInfoFabrica.SingleQustion),
                     singleNodeFabrica.GetNode("H", nodeInfoFabrica.GridQuestion, new[]
                    {
                         singleNodeFabrica.GetNode("T", nodeInfoFabrica.SingleQustion),
                         singleNodeFabrica.GetNode("R", nodeInfoFabrica.SingleQustion)
                    })
                })
            });

            return new SingleTree<StringId>(tree);
        }

        public static SingleTree<StringId> GetMinorTree(BindingHandler<StringId> bindingHandler)
        {
            var nodeInfoFabrica = new NodeInfoFabrica();
             var singleNodeFabrica = new SingleNodeFabrica();

             var tree = singleNodeFabrica.GetNode("A", nodeInfoFabrica.Root, new[]
            {
                 singleNodeFabrica.GetNode("B", nodeInfoFabrica.SingleQustion),
                 singleNodeFabrica.GetNode("C", nodeInfoFabrica.GridQuestion, new[]
                {
                     singleNodeFabrica.GetNode("D", nodeInfoFabrica.OpenTextInfo),
                     singleNodeFabrica.GetNode("E", nodeInfoFabrica.SingleQustion)
                }),
                 singleNodeFabrica.GetNode("F", nodeInfoFabrica.GridQuestion, new[]
                {
                     singleNodeFabrica.GetNode("G", nodeInfoFabrica.SingleQustion),
                     singleNodeFabrica.GetNode("H", nodeInfoFabrica.GridQuestion, new[]
                    {
                         singleNodeFabrica.GetNode("T", nodeInfoFabrica.SingleQustion),
                         singleNodeFabrica.GetNode("R", nodeInfoFabrica.SingleQustion)
                    })
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }
}