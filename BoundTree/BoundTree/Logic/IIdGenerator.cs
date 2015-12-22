namespace BoundTree.Logic
{
    public interface IIdGenerator<T>
    {
        T GetNewId();
    }
}