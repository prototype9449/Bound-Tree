using BoundTree.Helpers;
using BoundTree.Logic;

namespace ConsoleAppForTesting
{
    public class TreeFactory
    {
        public SingleTree<StringId> GetTree(ITree tree)
        {
            return tree.GetTree();
        }
    }

    public interface ITree
    {
        SingleTree<StringId> GetTree();
    }

    internal class Tree1 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFabrica = new NodeInfoFabrica();
            var singleNodeFabrica = new SingleNodeFabrica();

            var tree = singleNodeFabrica.GetNode("A", nodeInfoFabrica.Root, new[]
            {
                singleNodeFabrica.GetNode("B", nodeInfoFabrica.Single, new[]
                {
                    singleNodeFabrica.GetNode("C", nodeInfoFabrica.Answer)
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }

    internal class Tree2 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFabrica = new NodeInfoFabrica();
            var singleNodeFabrica = new SingleNodeFabrica();

            var tree = singleNodeFabrica.GetNode("A", nodeInfoFabrica.Root, new[]
            {
                singleNodeFabrica.GetNode("B", nodeInfoFabrica.Grid3D, new[]
                {
                    singleNodeFabrica.GetNode("C", nodeInfoFabrica.Grid, new[]
                    {
                        singleNodeFabrica.GetNode("D", nodeInfoFabrica.Single, new[]
                        {
                            singleNodeFabrica.GetNode("E", nodeInfoFabrica.PredefinedList, new[]
                            {
                                singleNodeFabrica.GetNode("F", nodeInfoFabrica.Answer)
                            })
                        })
                    })
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }

    public class Tree3 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFabrica = new NodeInfoFabrica();
            var singleNodeFabrica = new SingleNodeFabrica();

            var tree = singleNodeFabrica.GetNode("A", nodeInfoFabrica.Root, new[]
            {
                singleNodeFabrica.GetNode("B", nodeInfoFabrica.Single),
                singleNodeFabrica.GetNode("C", nodeInfoFabrica.Grid, new[]
                {
                    singleNodeFabrica.GetNode("D", nodeInfoFabrica.OpenText),
                    singleNodeFabrica.GetNode("E", nodeInfoFabrica.Single)
                }),
                singleNodeFabrica.GetNode("F", nodeInfoFabrica.Grid, new[]
                {
                    singleNodeFabrica.GetNode("G", nodeInfoFabrica.Single),
                    singleNodeFabrica.GetNode("H", nodeInfoFabrica.Grid, new[]
                    {
                        singleNodeFabrica.GetNode("T", nodeInfoFabrica.Single),
                        singleNodeFabrica.GetNode("R", nodeInfoFabrica.Single)
                    })
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }

    public class Tree4 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFabrica = new NodeInfoFabrica();
            var singleNodeFabrica = new SingleNodeFabrica();

            var tree = singleNodeFabrica.GetNode("A", nodeInfoFabrica.Root, new[]
            {
                singleNodeFabrica.GetNode("B", nodeInfoFabrica.Grid, new[]
                {
                    singleNodeFabrica.GetNode("C", nodeInfoFabrica.Single),
                    singleNodeFabrica.GetNode("D", nodeInfoFabrica.Single, new[]
                    {
                        singleNodeFabrica.GetNode("E", nodeInfoFabrica.PredefinedList),
                        singleNodeFabrica.GetNode("G", nodeInfoFabrica.Answer),
                        singleNodeFabrica.GetNode("K", nodeInfoFabrica.PredefinedList, new[]
                        {
                            singleNodeFabrica.GetNode("F", nodeInfoFabrica.PredefinedList),
                            singleNodeFabrica.GetNode("H", nodeInfoFabrica.Answer)
                        })
                    })
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }

    public class Tree5 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFabrica = new NodeInfoFabrica();
            var singleNodeFabrica = new SingleNodeFabrica();

            var tree = singleNodeFabrica.GetNode("A", nodeInfoFabrica.Root, new[]
            {
                singleNodeFabrica.GetNode("B", nodeInfoFabrica.Grid3D, new[]
                {
                    singleNodeFabrica.GetNode("C", nodeInfoFabrica.Grid, new[]
                    {
                        singleNodeFabrica.GetNode("D", nodeInfoFabrica.Single, new[]
                        {
                            singleNodeFabrica.GetNode("E", nodeInfoFabrica.PredefinedList, new[]
                            {
                                singleNodeFabrica.GetNode("G", nodeInfoFabrica.Answer)
                            })
                        }),
                        singleNodeFabrica.GetNode("H", nodeInfoFabrica.Single, new[]
                        {
                            singleNodeFabrica.GetNode("K", nodeInfoFabrica.Answer)
                        })
                    })
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }
}