namespace Stats
{
    public interface IModifier<TNumber> where TNumber : IStatNumber<TNumber>
    {
        ModifierType ModifierType { get; }
        TNumber Value { get; }
    }
}