using System.Collections.Generic;

namespace Stats
{
    public interface IRuntimeStats : IReadOnlyCollection<IRuntimeStat>
    {
        bool Contains(StatId statId);
        IRuntimeStat Get(StatId statId);
        IRuntimeStat<TNumber> Get<TNumber>(StatId<TNumber> statId) where TNumber : IStatNumber<TNumber>;
        internal void SyncWithTraitsClass(ITraitsClass traitsClass);
        internal void InitializeStartValues();
    }
}