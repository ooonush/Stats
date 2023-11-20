using System.Collections.Generic;

namespace Stats
{
    public interface IRuntimeStats : IReadOnlyCollection<IRuntimeStat>
    {
        IRuntimeStat<TNumber> Get<TNumber>(StatId<TNumber> statId) where TNumber : IStatNumber<TNumber>;
        bool Contains<TNumber>(StatId<TNumber> statId) where TNumber : IStatNumber<TNumber>;
    }
}