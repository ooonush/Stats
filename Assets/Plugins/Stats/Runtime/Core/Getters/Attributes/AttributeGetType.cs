using System;

namespace Stats
{
    [Serializable]
    public abstract class AttributeGetType<TNumber> : IGetType<IAttribute<TNumber>>, IGetType<IAttribute> where TNumber : IStatNumber<TNumber>
    {
        public abstract IAttribute<TNumber> Get();
        IAttribute IGetType<IAttribute>.Get() => Get();
    }
}