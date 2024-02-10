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

        public StatId<TNumber> StatId => _statId.Value;
        public TNumber Base => _base;
        public StatFormula<TNumber> Formula => _formula;
        StatId IStat.StatId => StatId;

        public Stat()
        {
        }

        public Stat(StatId<TNumber> statId, TNumber baseValue, StatFormula<TNumber> formula = null)
        {
            _statId = new StatIdItem<TNumber>(statId);
            _base = baseValue;
            _formula = formula;
        }
    }
}