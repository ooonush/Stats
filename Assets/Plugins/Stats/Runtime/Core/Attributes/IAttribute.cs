namespace Stats
{
    public interface IAttribute
    {
        AttributeId AttributeId { get; }
        IStat MaxValueStat { get; }
        float StartPercent { get; }
    }

    public interface IAttribute<TNumber> : IAttribute where TNumber : IStatNumber<TNumber>
    {
        new AttributeId<TNumber> AttributeId { get; }
        new IStat<TNumber> MaxValueStat { get; }
        TNumber MinValue { get; }
        new float StartPercent { get; }
    }
}