using UnityEngine;

namespace Stats
{
    public abstract class StatAsset<TNumber> : ScriptableObject, IStat<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private Stat<TNumber> _stat = new();

        public StatId<TNumber> StatId => _stat.StatId;
        public TNumber Base => _stat.Base;
        public StatFormula<TNumber> Formula => _stat.Formula;
        StatId IStat.StatId => StatId;
    }
}