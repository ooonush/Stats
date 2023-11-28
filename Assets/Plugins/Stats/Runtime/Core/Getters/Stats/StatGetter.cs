using System;

namespace Stats
{
    [Serializable]
    public sealed class StatGetter : Getter<IStat>
    {
        public StatGetter() => SetDefaultEditorPropertyType<StatValueGetType<TFloat>>();
    }

    [Serializable]
    public sealed class StatGetter<TNumber> : Getter<IStat<TNumber>> where TNumber : IStatNumber<TNumber>
    {
        public StatGetter() => SetDefaultEditorPropertyType<StatValueGetType<TNumber>>();
    }
}