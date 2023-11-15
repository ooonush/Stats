namespace Stats
{
    public interface IRuntimeAttribute<TNumber> where TNumber : IStatNumber<TNumber>
    {
        TNumber MinValue { get; }
        TNumber Value { get; set; }

        AttributeId<TNumber> AttributeId { get; }
        RuntimeStat<TNumber> MaxRuntimeStat { get; }
        event AttributeValueChangedAction<TNumber> OnValueChanged;
    }
}