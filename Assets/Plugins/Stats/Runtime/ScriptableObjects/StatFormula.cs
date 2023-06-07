using UnityEngine;

namespace Stats
{
    public abstract class StatFormula : ScriptableObject
    {
        public abstract float Calculate(IRuntimeStat stat, ITraits traits);
    }
}