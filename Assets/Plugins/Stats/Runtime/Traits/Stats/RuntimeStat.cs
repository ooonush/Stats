namespace Stats
{
    public sealed class RuntimeStat<TNumber> : RuntimeStatBase<TNumber> where TNumber : IStatNumber<TNumber>
    {
        internal RuntimeStat(ITraits traits, IStat<TNumber> stat) : base(traits, stat)
        {
        }
    }
}