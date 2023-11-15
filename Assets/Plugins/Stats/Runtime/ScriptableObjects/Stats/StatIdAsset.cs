using UnityEngine;

namespace Stats
{
    public abstract class StatIdAsset<TNumber> : ScriptableObject where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private StatId<TNumber> _statId;
        public StatId<TNumber> StatId => _statId;
    }
}