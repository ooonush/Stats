using System;

namespace Stats
{
    [Serializable]
    public sealed class RuntimeAttributes : RuntimeAttributesBase
    {
        internal RuntimeAttributes(Traits traits) : base(traits)
        {
        }

        protected override IRuntimeAttribute CreateRuntimeAttribute<TNumber>(IAttribute<TNumber> stat)
        {
            return new RuntimeAttribute<TNumber>(Traits, stat);
        }

        public new RuntimeAttribute<TNumber> Get<TNumber>(AttributeId<TNumber> stat) where TNumber : IStatNumber<TNumber>
        {
            return (RuntimeAttribute<TNumber>)base.Get(stat);
        }
    }
}