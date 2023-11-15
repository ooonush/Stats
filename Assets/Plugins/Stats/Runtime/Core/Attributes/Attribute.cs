using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public sealed class Attribute<TNumber> : IAttribute<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private AttributeIdItem<TNumber> _attributeId;
        [SerializeField] private TNumber _minValue;
        [SerializeField, Range(0, 1)] private float _startPercent = 1f;
        [SerializeField] private StatItem<TNumber> _maxValueStat;

        public AttributeId<TNumber> AttributeId => _attributeId.AttributeId;
        public IStat<TNumber> MaxValueStat => _maxValueStat.Stat;
        public TNumber MinValue => _minValue;
        public float StartPercent => _startPercent;
    }
}