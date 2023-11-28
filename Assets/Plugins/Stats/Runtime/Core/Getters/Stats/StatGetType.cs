using System;

namespace Stats
{
    [Serializable]
    public abstract class StatGetType<TNumber> : IGetType<IStat<TNumber>>, IGetType<IStat> where TNumber : IStatNumber<TNumber>
    {
        public abstract IStat<TNumber> Get();
        IStat IGetType<IStat>.Get() => Get();
    }
}