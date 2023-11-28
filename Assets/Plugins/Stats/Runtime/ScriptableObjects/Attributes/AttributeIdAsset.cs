using UnityEngine;

namespace Stats
{
    public abstract class AttributeIdAsset : ScriptableObject
    {
        [SerializeField] private AttributeId _attributeId;
        public AttributeId AttributeId => _attributeId;
    }

    public abstract class AttributeIdAsset<TNumber> : AttributeIdAsset where TNumber : IStatNumber<TNumber>
    {
        public new AttributeId<TNumber> AttributeId => base.AttributeId;
    }
}