#nullable enable
using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct TInt : IStatNumber<TInt>
    {
        [SerializeField] private int _value;

        public TInt(int value) => _value = value;

        TInt IAdditionOperators<TInt, TInt, TInt>.Add(TInt value) => this + value;
        TInt ISubtractionOperators<TInt, TInt, TInt>.Subtract(TInt value) => this - value;
        TInt IDivisionOperators<TInt, TInt, TInt>.Divide(TInt value) => this / value;
        TInt IMultiplyOperators<TInt, TInt, TInt>.Multiply(TInt value) => this * value;
        TInt IIncrementOperators<TInt>.Increment() => this++;
        TInt IDecrementOperators<TInt>.Decrement() => this--;
        bool IComparisonOperators<TInt, TInt, bool>.IsLess(TInt other) => this < other;
        bool IComparisonOperators<TInt, TInt, bool>.IsGreater(TInt other) => this > other;
        bool IComparisonOperators<TInt, TInt, bool>.IsGreaterOrEqual(TInt other) => this >= other;
        bool IComparisonOperators<TInt, TInt, bool>.IsLessOrEqual(TInt other) => this <= other;

        TInt IPercentageOperators<TInt, double, TInt>.CalculatePercent(double percent, RoundingMode roundingMode)
        {
            return (TInt)TMath.Round(_value * percent, roundingMode);
        }

        public bool Equals(TInt other) => _value.Equals(other._value);

        public int CompareTo(TInt other) => _value.CompareTo(other._value);

        public int CompareTo(object obj)
        {
            return obj switch
            {
                TInt other => CompareTo(other),
                int value => _value.CompareTo(value),
                _ => throw new NotImplementedException()
            };
        }

        #region Object Overrides

        public override bool Equals(object? obj) => obj is TInt other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();
        public override string ToString() => _value.ToString();

        #endregion

        #region Operators

        public static TInt operator +(TInt left, TInt right) => new(left._value + right._value);
        public static TInt operator -(TInt left, TInt right) => new(left._value - right._value);
        public static TInt operator /(TInt left, TInt right) => new(left._value / right._value);
        public static TInt operator *(TInt left, TInt right) => new(left._value * right._value);
        public static TInt operator ++(TInt value) => new(value._value + 1);
        public static TInt operator --(TInt value) => new(value._value - 1);
        public static bool operator <(TInt left, TInt right) => left._value < right._value;
        public static bool operator >(TInt left, TInt right) => left._value > right._value;
        public static bool operator >=(TInt left, TInt right) => left._value >= right._value;
        public static bool operator <=(TInt left, TInt right) => left._value <= right._value;
        public static bool operator ==(TInt left, TInt right) => left._value == right._value;
        public static bool operator !=(TInt left, TInt right) => left._value != right._value;

        #endregion

        #region Casting

        public static implicit operator int(TInt value) => value._value;
        public static implicit operator TInt(int value) => new(value);

        public static explicit operator uint(TInt value) => (uint)value._value;
        public static explicit operator TInt(uint value) => new((int)value);

        public static explicit operator ushort(TInt value) => (ushort)value._value;
        public static implicit operator TInt(ushort value) => new(value);

        public static explicit operator short(TInt value) => (short)value._value;
        public static implicit operator TInt(short value) => new(value);

        public static implicit operator double(TInt value) => value._value;
        public static explicit operator TInt(double value) => new((int)value);

        public static implicit operator long(TInt value) => value._value;
        public static explicit operator TInt(long value) => new((int)value);

        public static implicit operator float(TInt value) => value._value;
        public static explicit operator TInt(float value) => new((int)value);

        public static explicit operator byte(TInt value) => (byte)value._value;
        public static implicit operator TInt(byte value) => new(value);

        public static explicit operator sbyte(TInt value) => (sbyte)value._value;
        public static implicit operator TInt(sbyte value) => new(value);

        public static implicit operator decimal(TInt value) => value._value;
        public static explicit operator TInt(decimal value) => new((int)value);

        public static explicit operator ulong(TInt value) => (ulong)value._value;
        public static explicit operator TInt(ulong value) => new((int)value);

        public static explicit operator char(TInt value) => (char)value._value;
        public static implicit operator TInt(char value) => new(value);

        #endregion
    }
}