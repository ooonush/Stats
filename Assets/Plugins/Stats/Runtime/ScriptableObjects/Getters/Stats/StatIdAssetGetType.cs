using System;
using AInspector;
using UnityEngine;

namespace Stats
{
    [Serializable]
    [DropdownName("Asset")]
    internal abstract class StatIdAssetGetType<TNumber> : StatIdGetType<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private StatIdAsset<TNumber> _asset;
        public sealed override StatId<TNumber> Get() => _asset.StatId;
    }
}