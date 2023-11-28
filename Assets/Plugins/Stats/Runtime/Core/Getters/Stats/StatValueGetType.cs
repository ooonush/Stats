using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    internal abstract class StatValueGetType<TNumber> : StatGetType<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private Stat<TNumber> _stat;
        public override IStat<TNumber> Get() => _stat;
    }
}