using UnityEngine;

namespace Stats
{
    public abstract class AttributeAsset<TNumber> : ScriptableObject, IAttribute<TNumber>
        where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private Attribute<TNumber> _attribute;

        public AttributeId<TNumber> AttributeId => _attribute.AttributeId;
        public IStat<TNumber> MaxValueStat => _attribute.MaxValueStat; 
        public TNumber MinValue => _attribute.MinValue;
        public float StartPercent => _attribute.StartPercent;
    }
}