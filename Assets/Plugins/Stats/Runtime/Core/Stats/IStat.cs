namespace Stats
{
    public interface IStat
    {
        StatId StatId { get; }
    }

    public interface IStat<TNumber> : IStat where TNumber : IStatNumber<TNumber>
    {
        new StatId<TNumber> StatId { get; }
        TNumber Base { get; }
        StatFormula<TNumber> Formula { get; }
    }
}