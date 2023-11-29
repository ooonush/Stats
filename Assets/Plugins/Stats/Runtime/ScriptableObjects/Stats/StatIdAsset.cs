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
        public new StatId<TNumber> StatId => (StatId<TNumber>)base.StatId;
    }
}