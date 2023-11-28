using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct StatItem
    {
        [SerializeField] private StatGetter _stat;
        public IStat Stat => _stat.Get();
    }

    [Serializable]
    public struct StatItem<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private StatGetter<TNumber> _stat;
        public IStat<TNumber> Stat => _stat.Get();
    }
}