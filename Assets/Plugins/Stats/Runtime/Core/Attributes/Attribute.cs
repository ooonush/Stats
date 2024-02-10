﻿using System;
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

        public AttributeId<TNumber> AttributeId => _attributeId.Value;
        public IStat<TNumber> MaxValueStat => _maxValueStat.Value;
        public TNumber MinValue => _minValue;
        public float StartPercent => _startPercent;

        IStat IAttribute.MaxValueStat => MaxValueStat;
        AttributeId IAttribute.AttributeId => AttributeId;

        public Attribute()
        {
        }

        public Attribute(AttributeId<TNumber> attributeId, Stat<TNumber> maxValueStat, TNumber minValue = default, float startPercent = 1f)
        {
            _attributeId = new AttributeIdItem<TNumber>(attributeId);
            _minValue = minValue;
            _maxValueStat = new StatItem<TNumber>(maxValueStat);
            _startPercent = startPercent;
        }
    }
}