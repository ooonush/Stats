using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public sealed class Stat<TNumber> : IStat<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private StatIdItem<TNumber> _statId;
        [SerializeField] private TNumber _base;
        [SerializeField] private StatFormula<TNumber> _formula;

        public StatId<TNumber> StatId => _statId.StatId;
        public TNumber Base => _base;
        public StatFormula<TNumber> Formula => _formula;
        StatId IStat.StatId => StatId;
    }
}