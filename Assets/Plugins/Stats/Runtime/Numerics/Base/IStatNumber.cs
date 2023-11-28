namespace Stats
{
    public interface IStatNumber<TNumber> : 
        ITNumber<TNumber>,
        IPercentageOperators<TNumber, double, TNumber>,
        IDoubleConvertible
        where TNumber : IStatNumber<TNumber>
    {
    }
}