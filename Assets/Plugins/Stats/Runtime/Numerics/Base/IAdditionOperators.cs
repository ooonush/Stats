#nullable enable
namespace Stats
{
    public interface IAdditionOperators<TSelf, TOther, TResult> where TSelf : IAdditionOperators<TSelf, TOther, TResult>?
    {
        TResult Add(TOther value);
    }
}