#nullable enable
namespace Stats
{
    public interface IMultiplyOperators<TSelf, TOther, TResult> where TSelf : IMultiplyOperators<TSelf, TOther, TResult>?
    {
        TResult Multiply(TOther value);
    }
}