using System.Collections.Generic;

namespace Stats
{
    public interface IRuntimeStat
    {
        string StatId { get; }
        event StatValueChangedAction OnValueChanged;
        TDouble Base { get; }
        TDouble Value { get; }
        TDouble ModifiersValue { get; }
        IReadOnlyList<PercentageModifier> PercentageModifiers { get; }
        IReadOnlyList<ConstantModifier<TDouble>> ConstantModifiers { get; }
        internal void InitializeStartValues();
    }

    public interface IRuntimeStat<TNumber> : IRuntimeStat where TNumber : IStatNumber<TNumber>
    {
        new StatId<TNumber> StatId { get; }
        new TNumber Base { get; set; }
        new TNumber Value { get; }
        new TNumber ModifiersValue { get; }
        new IReadOnlyList<PercentageModifier> PercentageModifiers { get; }
        new IReadOnlyList<ConstantModifier<TNumber>> ConstantModifiers { get; }

        new event StatValueChangedAction<TNumber> OnValueChanged;

        void AddModifier(ConstantModifier<TNumber> modifier);
        void AddModifier(PercentageModifier modifier);
        bool RemoveModifier(ConstantModifier<TNumber> modifier);
        bool RemoveModifier(PercentageModifier modifier);
    }
}