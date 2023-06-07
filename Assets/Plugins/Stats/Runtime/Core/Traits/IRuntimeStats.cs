using System.Collections.Generic;

namespace Stats
{
    public interface IRuntimeStats<out TRuntimeStat> : IReadOnlyCollection<TRuntimeStat>
        where TRuntimeStat : IRuntimeStat
    {
        event StatValueChangedAction OnValueChanged;
        TRuntimeStat Get(StatType statType);
        bool Contains(StatType statType);
    }
}