using System.Collections.Generic;

namespace Stats
{
    public interface IRuntimeAttributes : IReadOnlyCollection<IRuntimeAttribute>
    {
        IRuntimeAttribute<TNumber> Get<TNumber>(AttributeId<TNumber> attributeId) where TNumber : IStatNumber<TNumber>;
        bool Contains<TNumber>(AttributeId<TNumber> attributeId) where TNumber : IStatNumber<TNumber>;
    }
}