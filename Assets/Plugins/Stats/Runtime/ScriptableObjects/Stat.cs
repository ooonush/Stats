using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(menuName = "Stats/Stat", fileName = "Stat", order = 0)]
    public sealed class Stat : ScriptableObject
    {
        [SerializeField] private StatType _type;
        [SerializeField] private float _base;
        [SerializeField] private StatFormula _formula;

        public StatType Type => _type;
        public float Base => _base;
        public StatFormula Formula => _formula;
    }
}