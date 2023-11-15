namespace Stats
{
    public interface IAttribute<TNumber> where TNumber : IStatNumber<TNumber>
    {
        AttributeId<TNumber> AttributeId { get; }
        IStat<TNumber> MaxValueStat { get; }
        TNumber MinValue { get; }
        float StartPercent { get; }
    }
}