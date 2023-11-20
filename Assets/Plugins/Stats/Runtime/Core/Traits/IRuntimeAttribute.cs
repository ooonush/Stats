using System;

namespace Stats
{
    public interface IRuntimeAttribute
    {
        string AttributeId { get; }
        event Action OnValueChanged;
    }

    public interface IRuntimeAttribute<TNumber> : IRuntimeAttribute where TNumber : IStatNumber<TNumber>
    {
        new AttributeId<TNumber> AttributeId { get; }
        TNumber MinValue { get; }
        TNumber Value { get; set; }
        IRuntimeStat<TNumber> MaxRuntimeStat { get; }

        new event AttributeValueChangedAction<TNumber> OnValueChanged;
    }
}