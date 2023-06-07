using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public sealed class AttributeItem
    {
        [SerializeField] private Attribute _attribute;

        [SerializeField] private bool _changeStartPercent;
        [SerializeField] [Range(0f, 1f)] private float _startPercent = 1f;

        public AttributeType AttributeType => _attribute.Type;

        public float MinValue => _attribute != null ? _attribute.MinValue : 0f;
        public StatType MaxValueType => _attribute != null ? AttributeType.MaxValueType : null;

        public float StartPercent
        {
            get
            {
                if (!_attribute) return 0f;
                return _changeStartPercent ? _startPercent : _attribute.StartPercent;
            }
        }

        public AttributeItem()
        {
        }

        public AttributeItem(Attribute attribute)
        {
            _attribute = attribute;
        }
    }
}