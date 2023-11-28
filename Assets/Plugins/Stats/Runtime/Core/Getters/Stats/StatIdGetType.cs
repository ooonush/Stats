using System;

namespace Stats
{
    [Serializable]
    public abstract class StatIdGetType : IGetType<StatId>
    {
        public abstract StatId Get();
    }

    [Serializable]
    public abstract class StatIdGetType<TNumber> : IGetType<StatId<TNumber>> where TNumber : IStatNumber<TNumber>
    {
        public abstract StatId<TNumber> Get();
    }
}