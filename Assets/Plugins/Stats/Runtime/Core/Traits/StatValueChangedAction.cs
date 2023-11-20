#nullable enable
namespace Stats
{
    public delegate void StatValueChangedAction<TNumber>(StatId<TNumber> statId, TNumber prev, TNumber next)
        where TNumber : IStatNumber<TNumber>;
}