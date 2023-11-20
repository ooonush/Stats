using System;

namespace Stats
{
    public static class TMath
    {
        public static TNumber Clamp<TNumber>(TNumber value, TNumber min, TNumber max)
            where TNumber : IStatNumber<TNumber>
        {
            if (min.IsGreater(max))
            {
                throw new ArgumentException("min must be less than max");
            }

            if (value.IsLess(min))
            {
                return min;
            }

            return value.IsGreater(max) ? max : value;
        }

        public static TNumber Lerp<TNumber>(TNumber min, TNumber max, float value,
            RoundingMode roundType = RoundingMode.Floor)
            where TNumber : IStatNumber<TNumber>
        {
            TNumber difference = max.Subtract(min);
            TNumber calculatedPercent = difference.CalculatePercent(value, roundType);
            return min.Add(calculatedPercent);
        }

        public static double Round(double value, RoundingMode roundType = RoundingMode.Round)
        {
            return roundType switch
            {
                RoundingMode.Floor => Math.Floor(value),
                RoundingMode.Ceiling => Math.Ceiling(value),
                RoundingMode.Round => Math.Round(value),
                RoundingMode.NotRound => value,
                _ => throw new ArgumentOutOfRangeException(nameof(roundType), roundType, null)
            };
        }
    }
}