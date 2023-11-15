namespace Stats
{
    public interface IComparisonOperators<TSelf, TOther, TResult>
        where TSelf : IComparisonOperators<TSelf, TOther, TResult>?
    {
        TResult IsLess(TOther other);
        TResult IsGreater(TOther other);
        TResult IsGreaterOrEqual(TOther other);
        TResult IsLessOrEqual(TOther other);
    }
}