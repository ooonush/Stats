#nullable enable
namespace Stats
{
    public delegate void StatValueChangedAction<TNumber>(StatId<TNumber> statId, TNumber change) where TNumber : IStatNumber<TNumber>;
}