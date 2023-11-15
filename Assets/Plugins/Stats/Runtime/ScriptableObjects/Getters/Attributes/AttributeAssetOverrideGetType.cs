using System;
using AInspector;
using UnityEngine;

namespace Stats
{
    [Serializable]
    [DropdownName("Asset Override")]
    internal abstract class AttributeAssetOverrideGetType<TNumber> : AttributeGetType<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private AttributeAssetOverride<TNumber> _attributeAssetOverride;
        public sealed override IAttribute<TNumber> Get() => _attributeAssetOverride;
    }
}