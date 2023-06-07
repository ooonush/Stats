using System;
using System.Collections.Generic;

namespace Stats
{
    [Serializable]
    public sealed class Modifiers
    {
        private ModifierList _percentages;
        private ModifierList _constants;

        public IReadOnlyList<Modifier> Percentages => _percentages;
        public IReadOnlyList<Modifier> Constants => _constants;

        public Modifiers()
        {
            _percentages = new ModifierList();
            _constants = new ModifierList();
        }

        public void CopyDataFrom(Modifiers modifiers)
        {
            Clear();

            foreach (Modifier modifier in modifiers.Percentages)
            {
                Add(ModifierType.Percent, modifier.Value);
            }
            foreach (Modifier modifier in modifiers.Constants)
            {
                Add(ModifierType.Constant, modifier.Value);
            }
        }

        public float Calculate(float value)
        {
            value *= 1f + _percentages.Value;
            value += _constants.Value;

            return value;
        }

        public Modifier Add(ModifierType type, float value)
        {
            return type switch
            {
                ModifierType.Constant => _constants.Add(value),
                ModifierType.Percent => _percentages.Add(value),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public bool Remove(ModifierType type, float value)
        {
            return type switch
            {
                ModifierType.Constant => _constants.Remove(value),
                ModifierType.Percent => _percentages.Remove(value),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public void Clear()
        {
            _percentages.Clear();
            _constants.Clear();
        }
    }
}