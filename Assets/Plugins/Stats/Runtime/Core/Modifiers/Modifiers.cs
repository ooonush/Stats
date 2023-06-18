using System;
using System.Collections.Generic;

namespace Stats
{
    public sealed class Modifiers
    {
        private readonly ModifierList _percentages = new();
        private readonly ModifierList _constants = new();

        public IReadOnlyList<Modifier> Percentages => _percentages;
        public IReadOnlyList<Modifier> Constants => _constants;

        public void CopyDataFrom(Modifiers modifiers)
        {
            _percentages.Clear();
            _constants.Clear();

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

        public void Add(ModifierType type, float value)
        {
            switch (type)
            {
                case ModifierType.Constant:
                    _constants.Add(new Modifier(type, value));
                    break;
                case ModifierType.Percent:
                    _percentages.Add(new Modifier(type, value));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
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
    }
}