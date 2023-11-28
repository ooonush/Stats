#nullable enable
using System;
using System.Globalization;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct TDouble : IStatNumber<TDouble>
    {
        [SerializeField] internal double Value;

        public TDouble(double value) => Value = value;

        TDouble IAdditionOperators<TDouble, TDouble, TDouble>.Add(TDouble value) => this + value;
        TDouble ISubtractionOperators<TDouble, TDouble, TDouble>.Subtract(TDouble value) => this - value;
        TDouble IDivisionOperators<TDouble, TDouble, TDouble>.Divide(TDouble value) => this / value;
        TDouble IMultiplyOperators<TDouble, TDouble, TDouble>.Multiply(TDouble value) => this * value;
        TDouble IIncrementOperators<TDouble>.Increment() => this++;
        TDouble IDecrementOperators<TDouble>.Decrement() => this--;
        bool IComparisonOperators<TDouble, TDouble, bool>.IsLess(TDouble other) => this < other;
        bool IComparisonOperators<TDouble, TDouble, bool>.IsGreater(TDouble other) => this > other;
        bool IComparisonOperators<TDouble, TDouble, bool>.IsGreaterOrEqual(TDouble other) => this >= other;
        bool IComparisonOperators<TDouble, TDouble, bool>.IsLessOrEqual(TDouble other) => this <= other;

        public bool Equals(TDouble other) => Value.Equals(other.Value);
        public int CompareTo(TDouble other) => Value.CompareTo(other.Value);

        public int CompareTo(object obj)
        {
            return obj switch
            {
                TDouble other => CompareTo(other),
                int value => Value.CompareTo(value),
                _ => throw new NotImplementedException()
            };
        }

        TDouble IPercentageOperators<TDouble, double, TDouble>.CalculatePercent(double percent, RoundingMode roundingMode)
        {
            return TMath.Round(Value * percent, roundingMode);
        }

        public TDouble ToDouble() => Value;

        #region Object Overrides

        public override bool Equals(object? obj) => obj is TDouble other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);

        #endregion

        #region Operators

        public static TDouble operator +(TDouble left, TDouble right) => new(left.Value + right.Value);
        public static TDouble operator -(TDouble left, TDouble right) => new(left.Value - right.Value);
        public static TDouble operator /(TDouble left, TDouble right) => new(left.Value / right.Value);
        public static TDouble operator *(TDouble left, TDouble right) => new(left.Value * right.Value);
        public static TDouble operator ++(TDouble value) => new(value.Value + 1);
        public static TDouble operator --(TDouble value) => new(value.Value - 1);
        public static bool operator <(TDouble left, TDouble right) => left.Value < right.Value;
        public static bool operator >(TDouble left, TDouble right) => left.Value > right.Value;
        public static bool operator >=(TDouble left, TDouble right) => left.Value >= right.Value;
        public static bool operator <=(TDouble left, TDouble right) => left.Value <= right.Value;
        public static bool operator ==(TDouble left, TDouble right) => left.Value == right.Value;
        public static bool operator !=(TDouble left, TDouble right) => left.Value != right.Value;

        #endregion

        #region Casting

        public static explicit operator int(TDouble value) => (int)value.Value;
        public static implicit operator TDouble(int value) => new(value);

        public static explicit operator uint(TDouble value) => (uint)value.Value;
        public static implicit operator TDouble(uint value) => new(value);

        public static explicit operator ushort(TDouble value) => (ushort)value.Value;
        public static implicit operator TDouble(ushort value) => new(value);

        public static explicit operator short(TDouble value) => (short)value.Value;
        public static implicit operator TDouble(short value) => new(value);

        public static implicit operator double(TDouble value) => value.Value;
        public static implicit operator TDouble(double value) => new(value);

        public static explicit operator long(TDouble value) => (long)value.Value;
        public static implicit operator TDouble(long value) => new(value);

        public static explicit operator float(TDouble value) => (float)value.Value;
        public static implicit operator TDouble(float value) => new(value);

        public static explicit operator byte(TDouble value) => (byte)value.Value;
        public static implicit operator TDouble(byte value) => new(value);

        public static explicit operator sbyte(TDouble value) => (sbyte)value.Value;
        public static implicit operator TDouble(sbyte value) => new(value);

        public static explicit operator decimal(TDouble value) => (decimal)value.Value;
        public static explicit operator TDouble(decimal value) => new((double)value);

        public static explicit operator ulong(TDouble value) => (ulong)value.Value;
        public static implicit operator TDouble(ulong value) => new(value);

        public static explicit operator char(TDouble value) => (char)value.Value;
        public static implicit operator TDouble(char value) => new(value);

        #endregion
    }
}