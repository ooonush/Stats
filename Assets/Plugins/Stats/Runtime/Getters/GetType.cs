namespace Stats
{
    public interface IGetType<out T>
    {
        public T Get();
    }
}