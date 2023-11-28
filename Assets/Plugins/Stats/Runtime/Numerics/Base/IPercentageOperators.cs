#nullable enable
namespace Stats
{
    public interface IPercentageOperators<TSelf, TOther, TResult> where TSelf : IPercentageOperators<TSelf, TOther, TResult>?
    {
        TResult CalculatePercent(TOther percent, RoundingMode roundingMode = RoundingMode.Floor);
    }
}