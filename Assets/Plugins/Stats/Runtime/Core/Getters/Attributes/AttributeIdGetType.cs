using System;

namespace Stats
{
    [Serializable]
    public abstract class AttributeIdGetType<TNumber> : GetType<AttributeId<TNumber>> where TNumber : IStatNumber<TNumber>
    {
    }
}