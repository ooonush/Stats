using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public sealed class TraitsClassAssetGetType : TraitsClassGetType
    {
        [SerializeField] private TraitsClassAsset _traitsClassAsset;
        public override ITraitsClass Get() => _traitsClassAsset;
    }
}