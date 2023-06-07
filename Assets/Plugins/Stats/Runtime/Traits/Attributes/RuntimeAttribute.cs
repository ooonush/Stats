using System;
using UnityEngine;

namespace Stats
{
    public sealed class RuntimeAttribute : IRuntimeAttribute
    {
        public readonly Traits Traits;
        public AttributeType AttributeType => _attributeItem.AttributeType;

        public float MaxValue => _attributeItem.MaxValueType != null
            ? Traits.RuntimeStats.Get(_attributeItem.MaxValueType).Value
            : 0f;

        public float MinValue { get; }

        private bool _initialized;
        private float _value;
        private readonly AttributeItem _attributeItem;

        public float Value
        {
            get
            {
                if (!_initialized)
                {
                    InitializeStartValues();
                }

                return _value;
            }
            set
            {
                float oldValue = Value;
                float newValue = Math.Clamp(value, MinValue, MaxValue);
                if (Math.Abs(_value - newValue) < float.Epsilon) return;

                _value = newValue;
                OnValueChanged?.Invoke(AttributeType, newValue - oldValue);
            }
        }

        public float Ratio => (Value - MinValue) / (MaxValue - MinValue);

        public event AttributeValueChangedAction OnValueChanged;

        public RuntimeAttribute(Traits traits, AttributeItem attributeItem)
        {
            Traits = traits;
            _attributeItem = attributeItem;

            MinValue = attributeItem.MinValue;

            if (attributeItem.MaxValueType)
            {
                traits.RuntimeStats.Get(attributeItem.MaxValueType).OnValueChanged += (_, _) => OnMaxValueChanged();
            }
        }

        internal void InitializeStartValues()
        {
            _initialized = true;
            _value = Mathf.Lerp(MinValue, MaxValue, _attributeItem.StartPercent);
        }

        private void OnMaxValueChanged()
        {
            float value = Math.Clamp(_value, MinValue, MaxValue);
            if (Math.Abs(Value - value) < float.Epsilon) return;

            Value = value;
        }
    }
}