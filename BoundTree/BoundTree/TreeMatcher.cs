using System;
using System.Linq;
using BoundTree.Logic.TreeNodes;
using BoundTree.Logic.Trees;
using BoundTree.TreeReconstruction;

namespace BoundTree
{
    //public class TreeMatcher<T> where T : class, IEquatable<T>, new()
    //{
    //    private readonly TreeConstructor<T> _treeConstructor = new TreeConstructor<T>();
    //    public MultiTree<T> GetDoubleNode(MultiTree<T> mainSingleTree, SingleTree<T> minorSingleTree)
    //    {
    //        var bindController = new BindContoller<T>(mainSingleTree, minorSingleTree);
    //        var mainIds = mainSingleTree.ToList().Select(node => node.Id);
    //        var minorIds = minorSingleTree.ToList().Select(node => node.SingleNodeData.Id);

    //        var identicalIds = mainIds.Intersect(minorIds).ToList();
    //        identicalIds.ForEach(id => bindController.Bind(id,id));

    //        return new MultiTree<T>(_treeConstructor.GetFilledTree(bindController).ToMultiNode());
    //    }
    //}
}