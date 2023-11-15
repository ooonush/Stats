using UnityEngine;

namespace Stats
{
    public abstract class AttributeIdAsset<TNumber> : ScriptableObject where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private AttributeId<TNumber> _attributeAttributeId;
        public AttributeId<TNumber> AttributeId => _attributeAttributeId;
    }
}