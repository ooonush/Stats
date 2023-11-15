using System.Collections.Generic;

namespace Stats
{
    public interface IRuntimeStat<TNumber> where TNumber : IStatNumber<TNumber>
    {
        StatId<TNumber> StatId { get; }
        TNumber Base { get; set; }
        TNumber Value { get; }
        TNumber ModifiersValue { get; }
        IReadOnlyList<ConstantModifier<TNumber>> PercentageModifiers { get; }
        IReadOnlyList<ConstantModifier<TNumber>> ConstantModifiers { get; }

        event StatValueChangedAction<TNumber> OnValueChanged;

        void AddModifier(ConstantModifier<TNumber> modifier);
        void AddModifier(PercentageModifier modifier);
        bool RemoveModifier(ConstantModifier<TNumber> modifier);
        bool RemoveModifier(PercentageModifier modifier);
    }
}