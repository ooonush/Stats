using System;
using AInspector;
using UnityEngine;

namespace Stats
{
    [Serializable]
    [DropdownName("Asset")]
    internal class StatIdAssetGetType : StatIdGetType
    {
        [SerializeField] private StatIdAsset _asset;
        public override StatId Get() => _asset.StatId;
    }

    [Serializable]
    [DropdownName("Asset")]
    internal abstract class StatIdAssetGetType<TNumber> : StatIdGetType<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private StatIdAsset<TNumber> _asset;
        public sealed override StatId<TNumber> Get() => _asset.StatId;
    }
}