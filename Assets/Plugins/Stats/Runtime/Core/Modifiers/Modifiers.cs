using System.Collections.Generic;

namespace Stats
{
    public sealed class Modifiers<TNumber> : IModifiers<TNumber> where TNumber : IStatNumber<TNumber>
    {
        private readonly PercentageModifierList _percentages = new();
        private readonly ConstantModifierList<TNumber> _constants = new();

        public IReadOnlyList<PercentageModifier> Percentages => _percentages;
        public IReadOnlyList<ConstantModifier<TNumber>> Constants => _constants;

        public void CopyDataFrom(Modifiers<TNumber> modifiers)
        {
            Clear();

            foreach (PercentageModifier modifier in modifiers.Percentages)
            {
                Add(modifier);
            }

            foreach (var modifier in modifiers.Constants)
            {
                Add(modifier);
            }
        }

        public TNumber Calculate(TNumber value)
        {
            value = value.Add(value.CalculatePercent(_percentages.PositiveValue - _percentages.NegativeValue));
            
            value = value.Add(_constants.PositiveValue);
            value = value.Add(_constants.NegativeValue);
            return value;
        }

        public void Add(PercentageModifier modifier) => _percentages.Add(modifier);

        public void Add(ConstantModifier<TNumber> modifier) => _constants.Add(modifier);

        public bool Remove(ConstantModifier<TNumber> modifier) => _constants.Remove(modifier);

        public bool Remove(PercentageModifier modifier) => _percentages.Remove(modifier);

        public void Clear()
        {
            _percentages.Clear();
            _constants.Clear();
        }
    }
}