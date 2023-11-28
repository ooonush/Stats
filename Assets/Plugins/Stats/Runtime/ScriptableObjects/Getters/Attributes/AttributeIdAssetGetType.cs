using System;
using AInspector;
using UnityEngine;

namespace Stats
{
    [Serializable]
    [DropdownName("Asset")]
    internal class AttributeIdAssetGetType : AttributeIdGetType
    {
        [SerializeField] private AttributeIdAsset _asset;
        public sealed override AttributeId Get() => _asset.AttributeId;
    }

    [Serializable]
    [DropdownName("Asset")]
    internal abstract class AttributeIdAssetGetType<TNumber> : AttributeIdGetType<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private AttributeIdAsset<TNumber> _asset;
        public sealed override AttributeId<TNumber> Get() => _asset.AttributeId;
    }
}