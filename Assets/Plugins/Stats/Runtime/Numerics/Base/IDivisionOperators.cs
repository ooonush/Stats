namespace Stats
{
    public interface IDivisionOperators<TSelf, TOther, TResult>
        where TSelf : IDivisionOperators<TSelf, TOther, TResult>?
    {
        TResult Divide(TOther value);
    }
}