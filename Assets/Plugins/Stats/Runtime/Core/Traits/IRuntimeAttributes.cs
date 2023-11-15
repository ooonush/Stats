namespace Stats
{
    public interface IRuntimeAttributes
    {
        IRuntimeAttribute<TNumber> Get<TNumber>(AttributeId<TNumber> attributeId) where TNumber : IStatNumber<TNumber>;
        bool Contains<TNumber>(AttributeId<TNumber> attributeId) where TNumber : IStatNumber<TNumber>;
    }
}