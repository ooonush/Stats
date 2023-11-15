using System;

namespace Stats
{
    [Serializable]
    public struct PercentageModifier : IModifier<TFloat>, IEquatable<PercentageModifier>
    {
        public ModifierType ModifierType { get; }
        public TFloat Value { get; }

        public PercentageModifier(TFloat value, ModifierType type = ModifierType.Positive)
        {
            Value = value;
            ModifierType = type;
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