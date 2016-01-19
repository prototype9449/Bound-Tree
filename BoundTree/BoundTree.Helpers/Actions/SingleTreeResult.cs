using System.Collections.Generic;
using BoundTree.Logic;
using BoundTree.Logic.Trees;

namespace BoundTree.Helpers.Actions
{
    public class SingleTreeResult : ILogResult
    {
        private readonly SingleTree<StringId> _singleTree;
        private readonly TreeConverter<StringId> _treeConverter = new TreeConverter<StringId>();

        public SingleTreeResult(SingleTree<StringId> singleTree)
        {
            _singleTree = singleTree;
        }

        public List<string> GetLines()
        {
            return _treeConverter.ConvertSingleTree(_singleTree);
        }
    }
}