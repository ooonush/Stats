using System;
using AInspector;
using UnityEngine;

namespace Stats
{
    [Serializable]
    [DropdownName("Asset")]
    public sealed class TraitsClassAssetGetType : TraitsClassGetType
    {
        [SerializeField] private TraitsClassAsset _traitsClassAsset;
        public override ITraitsClass Get() => _traitsClassAsset;

        public TraitsClassAssetGetType()
        {
        } 
        
        public TraitsClassAssetGetType(TraitsClassAsset traitsClassAsset)
        {
            _traitsClassAsset = traitsClassAsset;
        }
    }
}