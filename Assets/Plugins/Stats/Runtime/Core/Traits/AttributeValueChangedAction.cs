namespace Stats
{
    public delegate void AttributeValueChangedAction<TNumber>(AttributeId<TNumber> attributeId, TNumber prev, TNumber next) where TNumber : IStatNumber<TNumber>;
}