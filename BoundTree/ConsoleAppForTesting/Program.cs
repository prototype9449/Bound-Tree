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

        public static Tree<string> GetMainTree(BindingHandler<string> bindingHandler)
        {
            var fabrica = new NodeInfoFabrica();

            var tree = new Tree<string>(new Node<string>("A", fabrica.Root, new[]
            {
                new Node<string>("B", fabrica.SingleQustion),
                new Node<string>("C", fabrica.GridQuestion, new[]
                {
                    new Node<string>("D", fabrica.OpenTextInfo),
                    new Node<string>("E", fabrica.SingleQustion)
                }),
                new Node<string>("F", fabrica.GridQuestion, new[]
                {
                    new Node<string>("G", fabrica.SingleQustion),
                    new Node<string>("H", fabrica.GridQuestion, new[]
                    {
                        new Node<string>("T", fabrica.SingleQustion),
                        new Node<string>("R", fabrica.SingleQustion)
                    })
                })
            }));

            return tree;
        }

        public static Tree<string> GetMinorTree(BindingHandler<string> bindingHandler)
        {
            var fabrica = new NodeInfoFabrica();

            var tree = new Tree<string>(new Node<string>("A", fabrica.Root, new[]
            {
                new Node<string>("B", fabrica.SingleQustion),
                new Node<string>("C", fabrica.GridQuestion, new[]
                {
                    new Node<string>("D", fabrica.OpenTextInfo),
                    new Node<string>("E", fabrica.SingleQustion)
                }),
                new Node<string>("F", fabrica.GridQuestion, new[]
                {
                    new Node<string>("G", fabrica.SingleQustion),
                    new Node<string>("H", fabrica.GridQuestion, new[]
                    {
                        new Node<string>("T", fabrica.SingleQustion),
                        new Node<string>("R", fabrica.SingleQustion)
                    })
                })
            }));

            return tree;
        }
    }
}