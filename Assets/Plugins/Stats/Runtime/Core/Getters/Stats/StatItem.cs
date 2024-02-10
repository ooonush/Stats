using System;

namespace Stats
{
    [Serializable]
    public sealed class StatItem : Getter<IStat>
    {
        public StatItem() : base(new StatFloatValueGetType())
        {
        }
    }

    [Serializable]
    public sealed class StatItem<TNumber> : Getter<IStat<TNumber>> where TNumber : IStatNumber<TNumber>
    {
        public StatItem() => SetDefaultEditorPropertyType<StatValueGetType<TNumber>>();

        public StatItem(IStat<TNumber> stat) : base(stat)
        {
        }
    }
}