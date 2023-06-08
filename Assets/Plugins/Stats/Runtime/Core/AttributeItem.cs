using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public sealed class AttributeItem
    {
        [SerializeField] private AttributeType _attributeType;

        [SerializeField] private bool _changeStartPercent;
        [SerializeField] [Range(0f, 1f)] private float _startPercent = 1f;

        public AttributeType AttributeType => _attributeType;

        public float MinValue => _attributeType ? _attributeType.MinValue : 0f;
        public StatType MaxValueType => _attributeType ? _attributeType.MaxValueType : null;

        public float StartPercent
        {
            get
            {
                if (!_attributeType) return 0f;
                return _changeStartPercent ? _startPercent : _attributeType.StartPercent;
            }
        }
    }
}