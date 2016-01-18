namespace BoundTree.Interfaces
{
    public interface IID<T> where T : new()
    {
        bool IsEmpty();
    }
}