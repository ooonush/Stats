#nullable enable
using System;
using System.Globalization;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct TFloat : IStatNumber<TFloat>
    {
        [SerializeField] internal float Value;

        public TFloat(float value) => Value = value;

        TFloat IAdditionOperators<TFloat, TFloat, TFloat>.Add(TFloat value) => this + value;
        TFloat ISubtractionOperators<TFloat, TFloat, TFloat>.Subtract(TFloat value) => this - value;
        TFloat IDivisionOperators<TFloat, TFloat, TFloat>.Divide(TFloat value) => this / value;
        TFloat IMultiplyOperators<TFloat, TFloat, TFloat>.Multiply(TFloat value) => this * value;
        TFloat IIncrementOperators<TFloat>.Increment() => this++;
        TFloat IDecrementOperators<TFloat>.Decrement() => this--;
        bool IComparisonOperators<TFloat, TFloat, bool>.IsLess(TFloat other) => this < other;
        bool IComparisonOperators<TFloat, TFloat, bool>.IsGreater(TFloat other) => this > other;
        bool IComparisonOperators<TFloat, TFloat, bool>.IsGreaterOrEqual(TFloat other) => this >= other;
        bool IComparisonOperators<TFloat, TFloat, bool>.IsLessOrEqual(TFloat other) => this <= other;

        public bool Equals(TFloat other) => Value.Equals(other.Value);
        public int CompareTo(TFloat other) => Value.CompareTo(other.Value);

        public int CompareTo(object obj)
        {
            return obj switch
            {
                TFloat other => CompareTo(other),
                int value => Value.CompareTo(value),
                _ => throw new NotImplementedException()
            };
        }

        TFloat IPercentageOperators<TFloat, double, TFloat>.CalculatePercent(double percent, RoundingMode roundingMode)
        {
            return (TFloat)TMath.Round(Value * percent, roundingMode);
        }

        #region Object Overrides

        public override bool Equals(object? obj) => obj is TFloat other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);

        #endregion

        #region Operators

        public static TFloat operator +(TFloat left, TFloat right) => new(left.Value + right.Value);
        public static TFloat operator -(TFloat left, TFloat right) => new(left.Value - right.Value);
        public static TFloat operator /(TFloat left, TFloat right) => new(left.Value / right.Value);
        public static TFloat operator *(TFloat left, TFloat right) => new(left.Value * right.Value);
        public static TFloat operator ++(TFloat value) => new(value.Value + 1);
        public static TFloat operator --(TFloat value) => new(value.Value - 1);
        public static bool operator <(TFloat left, TFloat right) => left.Value < right.Value;
        public static bool operator >(TFloat left, TFloat right) => left.Value > right.Value;
        public static bool operator >=(TFloat left, TFloat right) => left.Value >= right.Value;
        public static bool operator <=(TFloat left, TFloat right) => left.Value <= right.Value;
        public static bool operator ==(TFloat left, TFloat right) => left.Value == right.Value;
        public static bool operator !=(TFloat left, TFloat right) => left.Value != right.Value;

        #endregion

        #region Casting

        public static explicit operator int(TFloat value) => (int)value.Value;
        public static implicit operator TFloat(int value) => new(value);

        public static explicit operator uint(TFloat value) => (uint)value.Value;
        public static explicit operator TFloat(uint value) => new(value);

        public static explicit operator ushort(TFloat value) => (ushort)value.Value;
        public static implicit operator TFloat(ushort value) => new(value);

        public static explicit operator short(TFloat value) => (short)value.Value;
        public static implicit operator TFloat(short value) => new(value);

        public static implicit operator double(TFloat value) => value.Value;
        public static explicit operator TFloat(double value) => new((float)value);

        public static explicit operator long(TFloat value) => (long)value.Value;
        public static implicit operator TFloat(long value) => new(value);

        public static implicit operator float(TFloat value) => value.Value;
        public static implicit operator TFloat(float value) => new(value);

        public static explicit operator byte(TFloat value) => (byte)value.Value;
        public static implicit operator TFloat(byte value) => new(value);

        public static explicit operator sbyte(TFloat value) => (sbyte)value.Value;
        public static implicit operator TFloat(sbyte value) => new(value);

        public static explicit operator decimal(TFloat value) => (decimal)value.Value;
        public static explicit operator TFloat(decimal value) => new((float)value);

        public static explicit operator ulong(TFloat value) => (ulong)value.Value;
        public static implicit operator TFloat(ulong value) => new(value);

        public static explicit operator char(TFloat value) => (char)value.Value;
        public static implicit operator TFloat(char value) => new(value);

        #endregion
    }
}