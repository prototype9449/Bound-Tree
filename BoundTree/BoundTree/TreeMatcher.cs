using System;
using System.Linq;
using BoundTree.Logic;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;
using BoundTree.TreeReconstruction;

namespace BoundTree
{
    public class TreeMatcher<T> where T : class, IEquatable<T>, new()
    {
        public DoubleNode<T> GetDoubleNode(SingleTree<T> mainSingleTree, SingleTree<T> minorSingleTree)
        {
            var bindController = new BindContoller<T>(mainSingleTree, minorSingleTree);
            var mainIds = mainSingleTree.ToList().Select(node => node.SingleNodeData.Id);
            var minorIds = minorSingleTree.ToList().Select(node => node.SingleNodeData.Id);

            var identicalIds = mainIds.Intersect(minorIds).ToList();
            identicalIds.ForEach(id => bindController.Bind(id,id));
            var treeReconstruction = new TreeReconstruction<T>(bindController);

            return treeReconstruction.GetFilledTree();
        }
    }
}