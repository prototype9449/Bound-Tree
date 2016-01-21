using System.Collections.Generic;
using BoundTree.Logic.TreeNodes;

namespace BoundTree.Logic
{
    public class IdGenerator: IIdGenerator<StringId>
    {
        private readonly HashSet<string> _ids = new HashSet<string>();

        public IdGenerator(IEnumerable<INode<StringId>> nodes)
        {
           nodes.ForEach(node => _ids.Add(node.Id.ToString()));
        }

        public StringId GetNewId()
        {
            var id = 0;
            while (_ids.Contains(id.ToString()))
            {
                id++;
            }
            _ids.Add(id.ToString());

            return new StringId(id.ToString());
        }
    }
}
