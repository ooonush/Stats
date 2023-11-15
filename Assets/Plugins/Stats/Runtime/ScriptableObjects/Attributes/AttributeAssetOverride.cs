﻿using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public sealed class AttributeAssetOverride<TNumber> : IAttribute<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private AttributeAsset<TNumber> _attributeAsset;

        [SerializeField] private bool _changeStartPercent;
        [SerializeField, Range(0, 1)] private float _startPercent;

        public AttributeId<TNumber> AttributeId => _attributeAsset.AttributeId;
        public IStat<TNumber> MaxValueStat => _attributeAsset.MaxValueStat;
        public TNumber MinValue => _attributeAsset ? _attributeAsset.MinValue : default;

        public float StartPercent
        {
            get
            {
                if (!_attributeAsset) return default;
                return _changeStartPercent ? _startPercent : _attributeAsset.StartPercent;
            }
        }
    }
}