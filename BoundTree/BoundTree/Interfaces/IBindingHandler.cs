namespace BoundTree.Interfaces
{
    public interface IBindingHandler<T>
    {
        void HandleBinding(Node<T> mainNode, Node<T> minorNode);
    }
}
