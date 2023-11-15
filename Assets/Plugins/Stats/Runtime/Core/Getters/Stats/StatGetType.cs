using System;

namespace Stats
{
    [Serializable]
    public abstract class StatGetType<TNumber> : GetType<IStat<TNumber>> where TNumber : IStatNumber<TNumber>
    {
    }
}