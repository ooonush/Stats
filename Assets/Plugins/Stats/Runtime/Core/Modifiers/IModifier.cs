namespace Stats
{
    public enum ModifierType : byte
    {
        Positive,
        Negative
    }

    public interface IModifier<TNumber> where TNumber : IStatNumber<TNumber>
    {
        ModifierType ModifierType { get; }
        TNumber Value { get; }
    }
}