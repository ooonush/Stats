namespace Stats
{
    public delegate void AttributeValueChangedAction<in TNumber>(string attributeId, TNumber change) where TNumber : IStatNumber<TNumber>;
}