using System.Collections.Generic;

namespace Stats
{
    public interface IRuntimeStat
    {
        StatType StatType { get; }
        float Base { get; set; }
        float Value { get; }
        float ModifiersValue { get; }
        IReadOnlyList<Modifier> PercentageModifiers { get; }
        IReadOnlyList<Modifier> ConstantModifiers { get; }

        event StatValueChangedAction OnValueChanged;

        void AddModifier(ModifierType modifierType, float value);
        bool RemoveModifier(ModifierType modifierType, float value);
    }
}