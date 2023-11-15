using System;

namespace Stats
{
    [Serializable]
    public abstract class AttributeGetType<TNumber> : GetType<IAttribute<TNumber>> where TNumber : IStatNumber<TNumber>
    {
    }
}