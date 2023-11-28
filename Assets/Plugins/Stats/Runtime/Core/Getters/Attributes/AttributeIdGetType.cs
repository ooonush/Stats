using System;

namespace Stats
{
    [Serializable]
    public abstract class AttributeIdGetType : IGetType<AttributeId>
    {
        public abstract AttributeId Get();
    }

    [Serializable]
    public abstract class AttributeIdGetType<TNumber> : IGetType<AttributeId<TNumber>> where TNumber : IStatNumber<TNumber>
    {
        public abstract AttributeId<TNumber> Get();
    }
}