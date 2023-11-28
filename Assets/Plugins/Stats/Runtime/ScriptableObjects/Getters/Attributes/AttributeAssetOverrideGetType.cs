using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    internal abstract class AttributeAssetOverrideGetType<TNumber> : AttributeGetType<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private AttributeAssetOverride<TNumber> _attributeAssetOverride;
        public override IAttribute<TNumber> Get() => _attributeAssetOverride;
    }
}