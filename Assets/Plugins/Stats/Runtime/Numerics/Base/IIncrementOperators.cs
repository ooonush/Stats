#nullable enable
namespace Stats
{
    public interface IIncrementOperators<TSelf> where TSelf : IIncrementOperators<TSelf>?
    {
        TSelf Increment();
    }
}