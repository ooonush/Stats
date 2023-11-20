using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct PercentageModifier : IModifier<TFloat>, IEquatable<PercentageModifier>
    {
        [SerializeField] private TFloat _value;
        [SerializeField] private ModifierType _modifierType;

        public TFloat Value => _value;
        public ModifierType ModifierType => _modifierType;

        public PercentageModifier(TFloat value, ModifierType type = ModifierType.Positive)
        {
            _value = value;
            _modifierType = type;
        }

        public bool Equals(PercentageModifier other)
        {
            return ModifierType == other.ModifierType && Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is PercentageModifier other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)ModifierType, Value);
        }
    }
}