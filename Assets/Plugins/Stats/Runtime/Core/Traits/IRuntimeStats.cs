namespace Stats
{
    public interface IRuntimeStats
    {
        IRuntimeStat<TNumber> Get<TNumber>(StatId<TNumber> statId) where TNumber : IStatNumber<TNumber>;
        bool Contains<TNumber>(StatId<TNumber> statIdAsset) where TNumber : IStatNumber<TNumber>;
    }
}