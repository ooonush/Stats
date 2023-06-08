using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(menuName = "Stats/Attribute Type", fileName = "Attribute Type", order = 0)]
    public sealed class AttributeType : IdScriptableObject
    {
        [SerializeField] private float _minValue;
        [SerializeField] private StatType _maxValueType;
        [Range(0f, 1f)]
        [SerializeField] private float _startPercent = 1f;

        public StatType MaxValueType => _maxValueType ? _maxValueType : null;
        public float MinValue => _minValue;
        public float StartPercent => _startPercent;
    }
}