using System.Collections.Generic;

namespace Stats
{
    public interface IRuntimeAttributes : IReadOnlyCollection<IRuntimeAttribute>
    {
        bool Contains(AttributeId attributeId);
        IRuntimeAttribute Get(AttributeId attributeId);
        IRuntimeAttribute<TNumber> Get<TNumber>(AttributeId<TNumber> attributeId) where TNumber : IStatNumber<TNumber>;
        internal void SyncWithTraitsClass(ITraitsClass traitsClass);
        internal void InitializeStartValues();
    }
}