using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(menuName = "Stats/Attribute", fileName = "Attribute", order = 0)]
    public sealed class Attribute : ScriptableObject
    {
        [SerializeField] private AttributeType _type;
        [SerializeField] private float _minValue;
        [Range(0f, 1f)]
        [SerializeField] private float _startPercent = 1;

        public float MinValue => _minValue;
        public AttributeType Type => _type;
        public float StartPercent => _startPercent;
    }
}