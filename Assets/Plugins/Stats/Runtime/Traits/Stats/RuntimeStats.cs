namespace Stats
{
    public sealed class RuntimeStats : RuntimeStatsBase
    {
        internal RuntimeStats(ITraits traits) : base(traits)
        {
        }

        protected override IRuntimeStat CreateRuntimeStat<TNumber>(IStat<TNumber> stat)
        {
            return new RuntimeStat<TNumber>(Traits, stat);
        }

        public new RuntimeStat<TNumber> Get<TNumber>(StatId<TNumber> statId) where TNumber : IStatNumber<TNumber>
        {
            return (RuntimeStat<TNumber>)base.Get(statId);
        }
    }
}