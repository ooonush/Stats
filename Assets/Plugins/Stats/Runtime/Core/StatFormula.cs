using UnityEngine;

namespace Stats
{
    public abstract class StatFormula<TNumber> : ScriptableObject where TNumber : IStatNumber<TNumber>
    {
        public abstract TNumber Calculate(IRuntimeStat<TNumber> stat, ITraits traits);
    }
}