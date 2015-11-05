using BoundTree;
using BoundTree.Helpers;
using BoundTree.NodeInfo;

namespace ConsoleAppForTesting
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bindingHandler = new BindingHandler<string>();
            var mainTree = GetMainTree(bindingHandler);
            var minorTree = GetMinorTree(bindingHandler);

            var bindController = new BindContoller<string>(mainTree, minorTree);

            new ConsoleController(bindController).Start();
        }

        public static SingleTree<string> GetMainTree(BindingHandler<string> bindingHandler)
        {
            var fabrica = new NodeInfoFabrica();

            var tree = new SingleTree<string>(new SingleNode<string>("A", fabrica.Root, new[]
            {
                new SingleNode<string>("B", fabrica.SingleQustion),
                new SingleNode<string>("C", fabrica.GridQuestion, new[]
                {
                    new SingleNode<string>("D", fabrica.OpenTextInfo),
                    new SingleNode<string>("E", fabrica.SingleQustion)
                }),
                new SingleNode<string>("F", fabrica.GridQuestion, new[]
                {
                    new SingleNode<string>("G", fabrica.SingleQustion),
                    new SingleNode<string>("H", fabrica.GridQuestion, new[]
                    {
                        new SingleNode<string>("T", fabrica.SingleQustion),
                        new SingleNode<string>("R", fabrica.SingleQustion)
                    })
                })
            }));

            return tree;
        }

        public static SingleTree<string> GetMinorTree(BindingHandler<string> bindingHandler)
        {
            var fabrica = new NodeInfoFabrica();

            var tree = new SingleTree<string>(new SingleNode<string>("A", fabrica.Root, new[]
            {
                new SingleNode<string>("B", fabrica.SingleQustion),
                new SingleNode<string>("C", fabrica.GridQuestion, new[]
                {
                    new SingleNode<string>("D", fabrica.OpenTextInfo),
                    new SingleNode<string>("E", fabrica.SingleQustion)
                }),
                new SingleNode<string>("F", fabrica.GridQuestion, new[]
                {
                    new SingleNode<string>("G", fabrica.SingleQustion),
                    new SingleNode<string>("H", fabrica.GridQuestion, new[]
                    {
                        new SingleNode<string>("T", fabrica.SingleQustion),
                        new SingleNode<string>("R", fabrica.SingleQustion)
                    })
                })
            }));

            return tree;
        }
    }
}