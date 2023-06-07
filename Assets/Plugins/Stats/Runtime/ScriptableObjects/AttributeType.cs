using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(menuName = "Stats/Attribute Type", fileName = "Attribute Type", order = 0)]
    public sealed class AttributeType : IdScriptableObject
    {
        [SerializeField] private StatType _maxValueType;
        public StatType MaxValueType => _maxValueType != null ? _maxValueType : null;
    }
}