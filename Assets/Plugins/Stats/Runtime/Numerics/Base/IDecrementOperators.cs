#nullable enable
namespace Stats
{
    public interface IDecrementOperators<TSelf> where TSelf : IDecrementOperators<TSelf>?
    {
        TSelf Decrement();
    }
}