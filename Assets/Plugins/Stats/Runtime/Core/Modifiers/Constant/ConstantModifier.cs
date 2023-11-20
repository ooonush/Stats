using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct ConstantModifier<TNumber> : IModifier<TNumber>, IEquatable<ConstantModifier<TNumber>> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private TNumber _value;
        [SerializeField] private ModifierType _modifierType;
        public TNumber Value => _value;
        public ModifierType ModifierType => _modifierType;

        public ConstantModifier(TNumber value, ModifierType type = ModifierType.Positive)
        {
            _value = value;
            _modifierType = type;
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