using System;
using System.Collections.Generic;

namespace Stats
{
    public struct ConstantModifier<TNumber> : IModifier<TNumber>, IEquatable<ConstantModifier<TNumber>> where TNumber : IStatNumber<TNumber>
    {
        public ModifierType ModifierType { get; }
        public TNumber Value { get; }

        public ConstantModifier(TNumber value, ModifierType type = ModifierType.Positive)
        {
            Value = value;
            ModifierType = type;
        }

        public bool Equals(ConstantModifier<TNumber> other)
        {
            return ModifierType == other.ModifierType && EqualityComparer<TNumber>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is ConstantModifier<TNumber> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)ModifierType, Value);
        }
    }
}