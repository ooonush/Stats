namespace Stats
{
    public interface IModifier<out TNumber> where TNumber : IStatNumber<TNumber>
    {
        ModifierType ModifierType { get; }
        TNumber Value { get; }
    }
}