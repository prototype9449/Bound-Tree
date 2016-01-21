using System.Collections.Generic;
using System.Diagnostics.Contracts;
using BoundTree.Logic.TreeNodes;

namespace BoundTree.Interfaces
{
    [ContractClass(typeof(BindingHandlerContract<>))]
    public interface IBindingHandler<T> where T : IId<T>, new()
    {
        bool HandleBinding(MultiNode<T> mainSingleNode, SingleNode<T> minorSingleNode);
        bool RemoveConnection(T mainId);
        List<KeyValuePair<T, T>> Connections { get; }
    }

    [ContractClassFor(typeof(IBindingHandler<>))]
    public abstract class BindingHandlerContract<T> : IBindingHandler<T> where T : IId<T>,  new()
    {
        public bool HandleBinding(MultiNode<T> mainSingleNode, SingleNode<T> minorSingleNode)
        {
            Contract.Requires(mainSingleNode != null);
            Contract.Requires(minorSingleNode != null);
            return default(bool);
        }

        public bool RemoveConnection(T mainId)
        {
            Contract.Requires(mainId != null);
            return default(bool);
        }

        [Pure]
        public List<KeyValuePair<T, T>> Connections
        {
            get { return default(List<KeyValuePair<T, T>>); }
        }
    }
}
