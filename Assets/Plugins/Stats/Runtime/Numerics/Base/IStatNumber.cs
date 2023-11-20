namespace Stats
{
    public interface IStatNumber<TNumber> : 
        ITNumber<TNumber>,
        IPercentageOperators<TNumber, double, TNumber>
        where TNumber : IStatNumber<TNumber>
    {
    }
}