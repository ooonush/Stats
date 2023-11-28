namespace Stats
{
    public sealed class Modifiers<TNumber> : IModifiers<TNumber> where TNumber : IStatNumber<TNumber>
    {
        public PercentageModifierList Percentages { get; } = new();

        public ConstantModifierList<TNumber> Constants { get; } = new();

        public void CopyDataFrom(Modifiers<TNumber> modifiers)
        {
            Clear();

            foreach (PercentageModifier modifier in modifiers.Percentages)
            {
                Add(modifier);
            }

            foreach (ConstantModifier<TNumber> modifier in modifiers.Constants)
            {
                Add(modifier);
            }
        }

        public TNumber Calculate(TNumber value)
        {
            value = value.Add(value.CalculatePercent(Percentages.PositiveValue - Percentages.NegativeValue));
            
            value = value.Add(Constants.PositiveValue);
            value = value.Add(Constants.NegativeValue);
            return value;
        }

        public void Add(PercentageModifier modifier) => Percentages.Add(modifier);

        public void Add(ConstantModifier<TNumber> modifier) => Constants.Add(modifier);

        public bool Remove(ConstantModifier<TNumber> modifier) => Constants.Remove(modifier);

        public bool Remove(PercentageModifier modifier) => Percentages.Remove(modifier);

        public void Clear()
        {
            Percentages.Clear();
            Constants.Clear();
        }
    }
}