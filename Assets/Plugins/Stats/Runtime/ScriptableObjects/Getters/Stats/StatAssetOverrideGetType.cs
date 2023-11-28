using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    internal abstract class StatAssetOverrideGetType<TNumber> : StatGetType<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private StatAssetOverride<TNumber> _statAssetOverride;
        public sealed override IStat<TNumber> Get() => _statAssetOverride;
    }
}