namespace Stats
{
    public interface IEqualityOperators<TSelf, TOther, TResult>
        where TSelf : IEqualityOperators<TSelf, TOther, TResult>?
    {
        TResult Equals(TOther other);
    }
}