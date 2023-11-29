namespace Stats
{
    public static class RuntimeStatsExtensions
    {
        public static IRuntimeStat Get(this IRuntimeStats runtimeStats, StatIdItem statIdItem)
        {
            return runtimeStats.Get(statIdItem.StatId);
        }

        public static IRuntimeStat<TNumber> Get<TNumber>(this IRuntimeStats runtimeStats, StatIdItem<TNumber> statIdItem) where TNumber : IStatNumber<TNumber>
        {
            return runtimeStats.Get(statIdItem.StatId);
        }

        public static IRuntimeStat<TNumber> Get<TNumber>(this IRuntimeStats runtimeStats, StatIdItem statIdItem) where TNumber : IStatNumber<TNumber>
        {
            return runtimeStats.Get((StatId<TNumber>)statIdItem.StatId);
        }

        public static RuntimeStat<TNumber> Get<TNumber>(this RuntimeStats runtimeStats, StatIdItem statIdItem) where TNumber : IStatNumber<TNumber>
        {
            return runtimeStats.Get((StatId<TNumber>)statIdItem.StatId);
        }

        public static RuntimeStat<TNumber> Get<TNumber>(this RuntimeStats runtimeStats, StatIdItem<TNumber> statIdItem) where TNumber : IStatNumber<TNumber>
        {
            return runtimeStats.Get(statIdItem.StatId);
        }
    }
}