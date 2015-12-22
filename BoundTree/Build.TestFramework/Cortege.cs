namespace Build.TestFramework
{
    public class Cortege<T, U>
    {
        public Cortege(T first, U second)
        {
            First = first;
            Second = second;
        }

        public T First { get; private set; }
        public U Second { get; private set; }
    }
}