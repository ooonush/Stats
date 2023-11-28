namespace Stats
{
    public delegate void AttributeValueChangedAction(string attributeId, TDouble prev, TDouble next);
    public delegate void AttributeValueChangedAction<TNumber>(AttributeId<TNumber> attributeId, TNumber prev, TNumber next) where TNumber : IStatNumber<TNumber>;
}