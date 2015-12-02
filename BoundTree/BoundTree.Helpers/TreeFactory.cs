using BoundTree.Logic;

namespace BoundTree.Helpers
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
            var nodeInfoFactory = new NodeInfoFactory();
            var singleNodeFactory = new SingleNodeFactory();

            var tree = singleNodeFactory.GetNode("A", nodeInfoFactory.Root, new[]
            {
                singleNodeFactory.GetNode("B", nodeInfoFactory.Single, new[]
                {
                    singleNodeFactory.GetNode("C", nodeInfoFactory.Answer)
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }

    internal class Tree2 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFactory = new NodeInfoFactory();
            var singleNodeFactory = new SingleNodeFactory();

            var tree = singleNodeFactory.GetNode("A", nodeInfoFactory.Root, new[]
            {
                singleNodeFactory.GetNode("B", nodeInfoFactory.Grid3D, new[]
                {
                    singleNodeFactory.GetNode("C", nodeInfoFactory.Grid, new[]
                    {
                        singleNodeFactory.GetNode("D", nodeInfoFactory.Single, new[]
                        {
                            singleNodeFactory.GetNode("E", nodeInfoFactory.PredefinedList, new[]
                            {
                                singleNodeFactory.GetNode("F", nodeInfoFactory.Answer)
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
            var nodeInfoFactory = new NodeInfoFactory();
            var singleNodeFactory = new SingleNodeFactory();

            var tree = singleNodeFactory.GetNode("A", nodeInfoFactory.Root, new[]
            {
                singleNodeFactory.GetNode("B", nodeInfoFactory.Single),
                singleNodeFactory.GetNode("C", nodeInfoFactory.Grid, new[]
                {
                    singleNodeFactory.GetNode("D", nodeInfoFactory.OpenText),
                    singleNodeFactory.GetNode("E", nodeInfoFactory.Single)
                }),
                singleNodeFactory.GetNode("F", nodeInfoFactory.Grid, new[]
                {
                    singleNodeFactory.GetNode("G", nodeInfoFactory.Single),
                    singleNodeFactory.GetNode("H", nodeInfoFactory.Grid, new[]
                    {
                        singleNodeFactory.GetNode("T", nodeInfoFactory.Single),
                        singleNodeFactory.GetNode("R", nodeInfoFactory.Single)
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
            var nodeInfoFactory = new NodeInfoFactory();
            var singleNodeFactory = new SingleNodeFactory();

            var tree = singleNodeFactory.GetNode("A", nodeInfoFactory.Root, new[]
            {
                singleNodeFactory.GetNode("B", nodeInfoFactory.Grid, new[]
                {
                    singleNodeFactory.GetNode("C", nodeInfoFactory.Single),
                    singleNodeFactory.GetNode("D", nodeInfoFactory.Single, new[]
                    {
                        singleNodeFactory.GetNode("E", nodeInfoFactory.PredefinedList),
                        singleNodeFactory.GetNode("G", nodeInfoFactory.Answer),
                        singleNodeFactory.GetNode("K", nodeInfoFactory.PredefinedList, new[]
                        {
                            singleNodeFactory.GetNode("F", nodeInfoFactory.PredefinedList),
                            singleNodeFactory.GetNode("H", nodeInfoFactory.Answer)
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
            var nodeInfoFactory = new NodeInfoFactory();
            var singleNodeFactory = new SingleNodeFactory();

            var tree = singleNodeFactory.GetNode("A", nodeInfoFactory.Root, new[]
            {
                singleNodeFactory.GetNode("B", nodeInfoFactory.Grid3D, new[]
                {
                    singleNodeFactory.GetNode("C", nodeInfoFactory.Grid, new[]
                    {
                        singleNodeFactory.GetNode("D", nodeInfoFactory.Single, new[]
                        {
                            singleNodeFactory.GetNode("E", nodeInfoFactory.PredefinedList, new[]
                            {
                                singleNodeFactory.GetNode("G", nodeInfoFactory.Answer)
                            })
                        }),
                        singleNodeFactory.GetNode("H", nodeInfoFactory.Single, new[]
                        {
                            singleNodeFactory.GetNode("K", nodeInfoFactory.Answer)
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
            var nodeInfoFactory = new NodeInfoFactory();
            var singleNodeFactory = new SingleNodeFactory();

            var tree = singleNodeFactory.GetNode("A", nodeInfoFactory.Root, new[]
            {
                singleNodeFactory.GetNode("B", nodeInfoFactory.Single, new[]
                {
                    singleNodeFactory.GetNode("C", nodeInfoFactory.Answer),
                    singleNodeFactory.GetNode("D", nodeInfoFactory.Answer),
                    singleNodeFactory.GetNode("E", nodeInfoFactory.Answer)
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }

    public class Tree7 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFactory = new NodeInfoFactory();
            var singleNodeFactory = new SingleNodeFactory();

            var tree = singleNodeFactory.GetNode("A", nodeInfoFactory.Root, new[]
            {
                singleNodeFactory.GetNode("C", nodeInfoFactory.Single, new[]
                {
                    singleNodeFactory.GetNode("B", nodeInfoFactory.Answer),
                    singleNodeFactory.GetNode("D", nodeInfoFactory.PredefinedList, new[]
                    {
                        singleNodeFactory.GetNode("E", nodeInfoFactory.Answer)
                    }),
                    singleNodeFactory.GetNode("G", nodeInfoFactory.PredefinedList, new[]
                    {
                        singleNodeFactory.GetNode("F", nodeInfoFactory.Answer),
                        singleNodeFactory.GetNode("K", nodeInfoFactory.PredefinedList, new[]
                        {
                            singleNodeFactory.GetNode("H", nodeInfoFactory.Answer)
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
            var nodeInfoFactory = new NodeInfoFactory();
            var singleNodeFactory = new SingleNodeFactory();

            var tree = singleNodeFactory.GetNode("A", nodeInfoFactory.Root, new[]
            {
                singleNodeFactory.GetNode("B", nodeInfoFactory.Single, new[]
                {
                    singleNodeFactory.GetNode("C", nodeInfoFactory.PredefinedList, new[]
                    {
                        singleNodeFactory.GetNode("D", nodeInfoFactory.PredefinedList),
                        singleNodeFactory.GetNode("E", nodeInfoFactory.PredefinedList)
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
            var nodeInfoFactory = new NodeInfoFactory();
            var singleNodeFactory = new SingleNodeFactory();

            var tree = singleNodeFactory.GetNode("A", nodeInfoFactory.Root, new[]
            {
                singleNodeFactory.GetNode("B", nodeInfoFactory.Single, new[]
                {
                    singleNodeFactory.GetNode("C", nodeInfoFactory.PredefinedList, new[]
                    {
                        singleNodeFactory.GetNode("D", nodeInfoFactory.PredefinedList),
                        singleNodeFactory.GetNode("E", nodeInfoFactory.PredefinedList)
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
            var nodeInfoFactory = new NodeInfoFactory();
            var singleNodeFactory = new SingleNodeFactory();

            var tree = singleNodeFactory.GetNode("A", nodeInfoFactory.Root, new[]
            {
                singleNodeFactory.GetNode("B", nodeInfoFactory.Grid3D, new[]
                {
                    singleNodeFactory.GetNode("C", nodeInfoFactory.Single, new[]
                    {
                        singleNodeFactory.GetNode("D", nodeInfoFactory.Answer),
                    })
                }),
                singleNodeFactory.GetNode("E", nodeInfoFactory.Grid3D, new[]
                {
                    singleNodeFactory.GetNode("F", nodeInfoFactory.Single, new[]
                    {
                        singleNodeFactory.GetNode("G", nodeInfoFactory.Answer),
                    })
                }),
                singleNodeFactory.GetNode("R", nodeInfoFactory.Grid3D, new[]
                {
                    singleNodeFactory.GetNode("T", nodeInfoFactory.Single)
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }

    public class Tree11 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFactory = new NodeInfoFactory();
            var singleNodeFactory = new SingleNodeFactory();

            var tree = singleNodeFactory.GetNode("A", nodeInfoFactory.Root, new[]
            {
                singleNodeFactory.GetNode("B", nodeInfoFactory.Grid, new[]
                {
                    singleNodeFactory.GetNode("C", nodeInfoFactory.Single, new[]
                    {
                        singleNodeFactory.GetNode("D", nodeInfoFactory.Answer),
                    })
                }),
                singleNodeFactory.GetNode("E", nodeInfoFactory.Grid, new[]
                {
                    singleNodeFactory.GetNode("F", nodeInfoFactory.Single, new[]
                    {
                        singleNodeFactory.GetNode("G", nodeInfoFactory.Answer),
                    })
                }),
                singleNodeFactory.GetNode("R", nodeInfoFactory.Grid, new[]
                {
                    singleNodeFactory.GetNode("T", nodeInfoFactory.Single)
                })
            });

            return new SingleTree<StringId>(tree);
        }
    }


    public class Tree12 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFactory = new NodeInfoFactory();
            var singleNodeFactory = new SingleNodeFactory();

            var tree = singleNodeFactory.GetNode("A", nodeInfoFactory.Root, new[]
            {
                singleNodeFactory.GetNode("B", nodeInfoFactory.Grid3D, new[]
                {
                    singleNodeFactory.GetNode("C", nodeInfoFactory.Single),
                    singleNodeFactory.GetNode("D", nodeInfoFactory.Multi)
                }),
                singleNodeFactory.GetNode("E", nodeInfoFactory.OpenText)
            });

            return new SingleTree<StringId>(tree);
        }
    }

    public class Tree13 : ITree
    {
        public SingleTree<StringId> GetTree()
        {
            var nodeInfoFactory = new NodeInfoFactory();
            var singleNodeFactory = new SingleNodeFactory();

            var tree = singleNodeFactory.GetNode("A", nodeInfoFactory.Root, new[]
                    {
                        singleNodeFactory.GetNode("B", nodeInfoFactory.Grid3D, new[]
                        {
                            singleNodeFactory.GetNode("C", nodeInfoFactory.Single),
                            singleNodeFactory.GetNode("D", nodeInfoFactory.Multi)
                        }),
                        singleNodeFactory.GetNode("E", nodeInfoFactory.OpenText)
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
