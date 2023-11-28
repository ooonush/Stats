using UnityEngine;

namespace Stats
{
    public abstract class StatIdAsset : ScriptableObject
    {
        [SerializeField] private StatId _statId;
        public StatId StatId => _statId;
    }

    public abstract class StatIdAsset<TNumber> : StatIdAsset where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private StatId<TNumber> _statId;
        public new StatId<TNumber> StatId => _statId;
    }
}