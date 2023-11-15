using System;
using AInspector;
using UnityEngine;

namespace Stats
{
    [Serializable]
    [DropdownName("Asset Override")]
    internal abstract class StatAssetOverrideGetType<TNumber> : StatGetType<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private StatAssetOverride<TNumber> _statAssetOverride;
        public sealed override IStat<TNumber> Get() => _statAssetOverride;
    }
}