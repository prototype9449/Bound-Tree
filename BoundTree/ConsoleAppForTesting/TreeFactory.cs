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
            var nodeInfoFabrica = new NodeInfoFactory();
            var singleNodeFabrica = new SingleNodeFactory();

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
            var nodeInfoFabrica = new NodeInfoFactory();
            var singleNodeFabrica = new SingleNodeFactory();

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
            var nodeInfoFabrica = new NodeInfoFactory();
            var singleNodeFabrica = new SingleNodeFactory();

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
            var nodeInfoFabrica = new NodeInfoFactory();
            var singleNodeFabrica = new SingleNodeFactory();

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
            var nodeInfoFabrica = new NodeInfoFactory();
            var singleNodeFabrica = new SingleNodeFactory();

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

    public class Tree6 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFabrica = new NodeInfoFactory();
            var singleNodeFabrica = new SingleNodeFactory();

            var tree = singleNodeFabrica.GetNode("A", nodeInfoFabrica.Root, new[]
            {
                singleNodeFabrica.GetNode("B", nodeInfoFabrica.Single, new[]
                {
                    singleNodeFabrica.GetNode("C", nodeInfoFabrica.Answer),
                    singleNodeFabrica.GetNode("D", nodeInfoFabrica.Answer),
                    singleNodeFabrica.GetNode("E", nodeInfoFabrica.Answer)
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }

    public class Tree7 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFabrica = new NodeInfoFactory();
            var singleNodeFabrica = new SingleNodeFactory();

            var tree = singleNodeFabrica.GetNode("A", nodeInfoFabrica.Root, new[]
            {
                singleNodeFabrica.GetNode("C", nodeInfoFabrica.Single, new[]
                {
                    singleNodeFabrica.GetNode("B", nodeInfoFabrica.Answer),
                    singleNodeFabrica.GetNode("D", nodeInfoFabrica.PredefinedList, new[]
                    {
                        singleNodeFabrica.GetNode("E", nodeInfoFabrica.Answer)
                    }),
                    singleNodeFabrica.GetNode("G", nodeInfoFabrica.PredefinedList, new[]
                    {
                        singleNodeFabrica.GetNode("F", nodeInfoFabrica.Answer),
                        singleNodeFabrica.GetNode("K", nodeInfoFabrica.PredefinedList, new[]
                        {
                            singleNodeFabrica.GetNode("H", nodeInfoFabrica.Answer)
                        })
                    })
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }

    public class Tree8 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFabrica = new NodeInfoFactory();
            var singleNodeFabrica = new SingleNodeFactory();

            var tree = singleNodeFabrica.GetNode("A", nodeInfoFabrica.Root, new[]
            {
                singleNodeFabrica.GetNode("B", nodeInfoFabrica.Single, new[]
                {
                    singleNodeFabrica.GetNode("C", nodeInfoFabrica.PredefinedList, new[]
                    {
                        singleNodeFabrica.GetNode("D", nodeInfoFabrica.PredefinedList),
                        singleNodeFabrica.GetNode("E", nodeInfoFabrica.PredefinedList)
                    })
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }

    public class Tree9 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFabrica = new NodeInfoFactory();
            var singleNodeFabrica = new SingleNodeFactory();

            var tree = singleNodeFabrica.GetNode("A", nodeInfoFabrica.Root, new[]
            {
                singleNodeFabrica.GetNode("B", nodeInfoFabrica.Single, new[]
                {
                    singleNodeFabrica.GetNode("C", nodeInfoFabrica.PredefinedList, new[]
                    {
                        singleNodeFabrica.GetNode("D", nodeInfoFabrica.PredefinedList),
                        singleNodeFabrica.GetNode("E", nodeInfoFabrica.PredefinedList)
                    })
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }

    public class Tree10 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFabrica = new NodeInfoFactory();
            var singleNodeFabrica = new SingleNodeFactory();

            var tree = singleNodeFabrica.GetNode("A", nodeInfoFabrica.Root, new[]
            {
                singleNodeFabrica.GetNode("B", nodeInfoFabrica.Grid3D, new[]
                {
                    singleNodeFabrica.GetNode("C", nodeInfoFabrica.Single, new[]
                    {
                        singleNodeFabrica.GetNode("D", nodeInfoFabrica.Answer),
                    })
                }),
                singleNodeFabrica.GetNode("E", nodeInfoFabrica.Grid3D, new[]
                {
                    singleNodeFabrica.GetNode("F", nodeInfoFabrica.Single, new[]
                    {
                        singleNodeFabrica.GetNode("G", nodeInfoFabrica.Answer),
                    })
                }),
                singleNodeFabrica.GetNode("R", nodeInfoFabrica.Grid3D, new[]
                {
                    singleNodeFabrica.GetNode("T", nodeInfoFabrica.Single)
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }

    public class Tree11 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFabrica = new NodeInfoFactory();
            var singleNodeFabrica = new SingleNodeFactory();

            var tree = singleNodeFabrica.GetNode("A", nodeInfoFabrica.Root, new[]
            {
                singleNodeFabrica.GetNode("B", nodeInfoFabrica.Grid, new[]
                {
                    singleNodeFabrica.GetNode("C", nodeInfoFabrica.Single, new[]
                    {
                        singleNodeFabrica.GetNode("D", nodeInfoFabrica.Answer),
                    })
                }),
                singleNodeFabrica.GetNode("E", nodeInfoFabrica.Grid, new[]
                {
                    singleNodeFabrica.GetNode("F", nodeInfoFabrica.Single, new[]
                    {
                        singleNodeFabrica.GetNode("G", nodeInfoFabrica.Answer),
                    })
                }),
                singleNodeFabrica.GetNode("R", nodeInfoFabrica.Grid, new[]
                {
                    singleNodeFabrica.GetNode("T", nodeInfoFabrica.Single)
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }


    public class Tree12 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFabrica = new NodeInfoFactory();
            var singleNodeFabrica = new SingleNodeFactory();

            var tree = singleNodeFabrica.GetNode("A", nodeInfoFabrica.Root, new[]
            {
                singleNodeFabrica.GetNode("B", nodeInfoFabrica.Grid3D, new[]
                {
                    singleNodeFabrica.GetNode("C", nodeInfoFabrica.Single),
                    singleNodeFabrica.GetNode("D", nodeInfoFabrica.Multi)
                }),
                singleNodeFabrica.GetNode("E", nodeInfoFabrica.OpenText)
            });

            return new SingleTree<StringId>(tree);
        }
    }

    public class Tree13 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFabrica = new NodeInfoFactory();
            var singleNodeFabrica = new SingleNodeFactory();

            var tree = singleNodeFabrica.GetNode("A", nodeInfoFabrica.Root, new[]
                    {
                        singleNodeFabrica.GetNode("B", nodeInfoFabrica.Grid3D, new[]
                        {
                            singleNodeFabrica.GetNode("C", nodeInfoFabrica.Single),
                            singleNodeFabrica.GetNode("D", nodeInfoFabrica.Multi)
                        }),
                        singleNodeFabrica.GetNode("E", nodeInfoFabrica.OpenText)
                    });

            return new SingleTree<StringId>(tree);
        }
    }


    // Root                  Root
    //    Grid3D           Grid3d
    //      Single       Single
    //      Multi  <---> Multi
    //    OpenText <---->  OpenText

}
