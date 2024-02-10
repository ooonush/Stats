namespace Stats
{
    public interface IRuntimeAttribute
    {
        string AttributeId { get; }
        TDouble MinValue { get; }
        TDouble Value { get; }
        IRuntimeStat MaxRuntimeStat { get; }

        event AttributeValueChangedAction OnValueChanged;

        protected internal void InitializeStartValues();
    }

    public interface IRuntimeAttribute<TNumber> : IRuntimeAttribute where TNumber : IStatNumber<TNumber>
    {
        new AttributeId<TNumber> AttributeId { get; }
        new TNumber MinValue { get; }
        new TNumber Value { get; set; }
        new IRuntimeStat<TNumber> MaxRuntimeStat { get; }

        new event AttributeValueChangedAction<TNumber> OnValueChanged;
    }
}