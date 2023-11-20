using System;
using System.Collections.Generic;

namespace Stats
{
    public interface IRuntimeStat
    {
        string StatId { get; }
        event Action OnChanged;
    }

    public interface IRuntimeStat<TNumber> : IRuntimeStat where TNumber : IStatNumber<TNumber>
    {
        new StatId<TNumber> StatId { get; }
        TNumber Base { get; set; }
        TNumber Value { get; }
        TNumber ModifiersValue { get; }
        IReadOnlyList<PercentageModifier> PercentageModifiers { get; }
        IReadOnlyList<ConstantModifier<TNumber>> ConstantModifiers { get; }

        new event StatValueChangedAction<TNumber> OnValueChanged;

        void AddModifier(ConstantModifier<TNumber> modifier);
        void AddModifier(PercentageModifier modifier);
        bool RemoveModifier(ConstantModifier<TNumber> modifier);
        bool RemoveModifier(PercentageModifier modifier);
    }
}