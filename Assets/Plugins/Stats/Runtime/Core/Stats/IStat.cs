namespace Stats
{
    public interface IStat<TNumber> where TNumber : IStatNumber<TNumber>
    {
        StatId<TNumber> StatId { get; }
        TNumber Base { get; }
        StatFormula<TNumber> Formula { get; }
    }
}