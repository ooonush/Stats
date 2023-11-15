using System;
using AInspector;
using UnityEngine;

namespace Stats
{
    [Serializable]
    [DropdownName("Stat")]
    internal abstract class StatValueGetType<TNumber> : StatGetType<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private Stat<TNumber> _stat;
        public sealed override IStat<TNumber> Get() => _stat;
    }
}