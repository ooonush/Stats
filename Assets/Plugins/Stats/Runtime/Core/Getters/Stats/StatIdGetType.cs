using System;

namespace Stats
{
    [Serializable]
    public abstract class StatIdGetType<TNumber> : GetType<StatId<TNumber>> where TNumber : IStatNumber<TNumber>
    {
    }
}