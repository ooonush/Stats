using System;
using System.Collections;
using System.Collections.Generic;

namespace Stats
{
    [Serializable]
    public sealed class RuntimeAttributes : IRuntimeAttributes<RuntimeAttribute>
    {
        private readonly Traits _traits;
        private readonly Dictionary<AttributeType, RuntimeAttribute> _attributes = new();

        public int Count => _attributes.Values.Count;

        public event AttributeValueChangedAction OnValueChanged;

        internal RuntimeAttributes(Traits traits)
        {
            _traits = traits;
        }

        public void SyncWithTraitsClass(TraitsClassBase traitsClass)
        {
            ClearAttributes();

            foreach (AttributeItem attributeItem in traitsClass.AttributeItems)
            {
                if (attributeItem == null || !attributeItem.AttributeType)
                {
                    throw new NullReferenceException("No AttributeType reference found in TraitsClass");
                }

                AttributeType attributeType = attributeItem.AttributeType;
                if (_attributes.ContainsKey(attributeType))
                {
                    throw new Exception($"Attribute with AttributeType \"{attributeType.name}\" already exists");
                }

                var runtimeAttribute = new RuntimeAttribute(_traits, attributeItem);

                runtimeAttribute.OnValueChanged += InvokeOnValueChanged;
                _attributes[attributeType] = runtimeAttribute;
            }
        }

        private void ClearAttributes()
        {
            var runtimeAttributes = new List<RuntimeAttribute>(_attributes.Values);
            foreach (RuntimeAttribute runtimeAttribute in runtimeAttributes)
            {
                runtimeAttribute.OnValueChanged -= InvokeOnValueChanged;
                _attributes.Remove(runtimeAttribute.AttributeType);
            }
        }

        private void InvokeOnValueChanged(AttributeType attributeType, float change)
        {
            OnValueChanged?.Invoke(attributeType, change);
        }

        public RuntimeAttribute Get(AttributeType attributeType)
        {
            if (attributeType == null) throw new ArgumentNullException(nameof(attributeType));

            try
            {
                return _attributes[attributeType];
            }
            catch (Exception exception)
            {
                throw new ArgumentException("AttributeType not found in RuntimeAttributes", nameof(attributeType),
                    exception);
            }
        }

        public bool Contains(AttributeType attributeType) => _attributes.ContainsKey(attributeType);

        public IEnumerator<RuntimeAttribute> GetEnumerator() => _attributes.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}