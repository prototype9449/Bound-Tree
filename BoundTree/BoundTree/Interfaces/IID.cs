namespace BoundTree.Interfaces
{
    public interface IId<T> where T : new()
    {
        bool IsEmpty();
    }
}