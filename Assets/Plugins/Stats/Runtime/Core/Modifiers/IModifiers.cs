namespace Stats
{
    public interface IModifiers<TNumber> where TNumber : IStatNumber<TNumber>
    {
        PercentageModifierList Percentages { get; }
        ConstantModifierList<TNumber> Constants { get; }
        void Add(PercentageModifier modifier);
        void Add(ConstantModifier<TNumber> modifier);
        bool Remove(ConstantModifier<TNumber> modifier);
        bool Remove(PercentageModifier modifier);
        void CopyDataFrom(Modifiers<TNumber> modifiers);
        TNumber Calculate(TNumber value);
    }
}