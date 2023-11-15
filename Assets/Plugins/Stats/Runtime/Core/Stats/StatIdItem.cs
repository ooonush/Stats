using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct StatIdItem<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private StatIdGetter<TNumber> _statId;
        public StatId<TNumber> StatId => _statId.Get();
    }
}