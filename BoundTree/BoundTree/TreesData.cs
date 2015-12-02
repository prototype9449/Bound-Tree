using BoundTree.Logic;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;

namespace BoundTree
{
    public class TreesData
    {
        public DoubleNode<StringId> DoubleNode { get; set; }
        public SingleTree<StringId> MainSingleTree { get; set; }
        public SingleTree<StringId> MinorSingleTree { get; set; }
        
        public TreesData(DoubleNode<StringId> doubleNode, SingleTree<StringId> mainSingleTree, SingleTree<StringId> minorSingleTree)
        {
            DoubleNode = doubleNode;
            MainSingleTree = mainSingleTree;
            MinorSingleTree = minorSingleTree;
        }
    }
}