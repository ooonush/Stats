using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct TraitsClassItem
    {
        [SerializeField] private TraitsClassGetter _traitsClass;
        public ITraitsClass TraitsClass => _traitsClass.Get();
    }
}