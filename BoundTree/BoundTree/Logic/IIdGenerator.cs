namespace BoundTree.TreeReconstruction
{
    public interface IIdGenerator<T>
    {
        T GetNewId();
    }
}