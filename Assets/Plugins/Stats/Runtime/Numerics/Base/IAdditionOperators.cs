#nullable enable
namespace Stats
{
    public interface IPercentageOperators<TSelf, TOther, TResult> where TSelf : IPercentageOperators<TSelf, TOther, TResult>?
    {
        TResult CalculatePercent(TOther percent, RoundingMode roundingMode = RoundingMode.Floor);
    }

    public interface IAdditionOperators<TSelf, TOther, TResult> where TSelf : IAdditionOperators<TSelf, TOther, TResult>?
    {
        TResult Add(TOther value);
    }
}