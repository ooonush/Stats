#nullable enable
using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct TInt : IStatNumber<TInt>
    {
        [SerializeField] internal int Value;

        public TInt(int value) => Value = value;

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
            return (TInt)TMath.Round(Value * percent, roundingMode);
        }

        public bool Equals(TInt other) => Value.Equals(other.Value);

        public int CompareTo(TInt other) => Value.CompareTo(other.Value);

        public int CompareTo(object obj)
        {
            return obj switch
            {
                TInt other => CompareTo(other),
                int value => Value.CompareTo(value),
                _ => throw new NotImplementedException()
            };
        }

        public TDouble ToDouble() => Value;

        #region Object Overrides

        public override bool Equals(object? obj) => obj is TInt other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();

        #endregion

        #region Operators

        public static TInt operator +(TInt left, TInt right) => new(left.Value + right.Value);
        public static TInt operator -(TInt left, TInt right) => new(left.Value - right.Value);
        public static TInt operator /(TInt left, TInt right) => new(left.Value / right.Value);
        public static TInt operator *(TInt left, TInt right) => new(left.Value * right.Value);
        public static TInt operator ++(TInt value) => new(value.Value + 1);
        public static TInt operator --(TInt value) => new(value.Value - 1);
        public static bool operator <(TInt left, TInt right) => left.Value < right.Value;
        public static bool operator >(TInt left, TInt right) => left.Value > right.Value;
        public static bool operator >=(TInt left, TInt right) => left.Value >= right.Value;
        public static bool operator <=(TInt left, TInt right) => left.Value <= right.Value;
        public static bool operator ==(TInt left, TInt right) => left.Value == right.Value;
        public static bool operator !=(TInt left, TInt right) => left.Value != right.Value;

        #endregion

        #region Casting

        public static implicit operator int(TInt value) => value.Value;
        public static implicit operator TInt(int value) => new(value);

        public static explicit operator uint(TInt value) => (uint)value.Value;
        public static explicit operator TInt(uint value) => new((int)value);

        public static explicit operator ushort(TInt value) => (ushort)value.Value;
        public static implicit operator TInt(ushort value) => new(value);

        public static explicit operator short(TInt value) => (short)value.Value;
        public static implicit operator TInt(short value) => new(value);

        public static implicit operator double(TInt value) => value.Value;
        public static explicit operator TInt(double value) => new((int)value);

        public static implicit operator long(TInt value) => value.Value;
        public static explicit operator TInt(long value) => new((int)value);

        public static implicit operator float(TInt value) => value.Value;
        public static explicit operator TInt(float value) => new((int)value);

        public static explicit operator byte(TInt value) => (byte)value.Value;
        public static implicit operator TInt(byte value) => new(value);

        public static explicit operator sbyte(TInt value) => (sbyte)value.Value;
        public static implicit operator TInt(sbyte value) => new(value);

        public static implicit operator decimal(TInt value) => value.Value;
        public static explicit operator TInt(decimal value) => new((int)value);

        public static explicit operator ulong(TInt value) => (ulong)value.Value;
        public static explicit operator TInt(ulong value) => new((int)value);

        public static explicit operator char(TInt value) => (char)value.Value;
        public static implicit operator TInt(char value) => new(value);

        #endregion
    }
}