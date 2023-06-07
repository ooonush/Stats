using System.Collections.Generic;

namespace Stats
{
    public interface IRuntimeStat
    {
        event StatValueChangedAction OnValueChanged;
        StatType StatType { get; }
        float Base { get; set; }
        float Value { get; }
        float ModifiersValue { get; }
        IReadOnlyList<Modifier> PercentageModifiers { get; }
        IReadOnlyList<Modifier> ConstantModifiers { get; }
        void AddModifier(ModifierType modifierType, float value);
        bool RemoveModifier(ModifierType modifierType, float value);
    }
}