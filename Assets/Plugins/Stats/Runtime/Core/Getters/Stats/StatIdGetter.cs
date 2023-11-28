using System;

namespace Stats
{
    [Serializable]
    internal sealed class StatIdGetter : Getter<StatId>
    {
        public StatIdGetter() : base(new StatIdValueGetType())
        {
        }
    }

    [Serializable]
    internal sealed class StatIdGetter<TNumber> : Getter<StatId<TNumber>> where TNumber : IStatNumber<TNumber>
    {
        public StatIdGetter() => SetDefaultEditorPropertyType<StatIdValueGetType<TNumber>>();
    }
}