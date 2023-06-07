using System;
using System.Collections;
using System.Collections.Generic;

namespace Stats
{
    [Serializable]
    public sealed class RuntimeAttributes : IRuntimeAttributes<RuntimeAttribute>
    {
        private readonly Traits _traits;
        private readonly Dictionary<AttributeType, RuntimeAttribute> _attributes = new Dictionary<AttributeType, RuntimeAttribute>();

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
                if (attributeItem == null || attributeItem.AttributeType == null)
                {
                    throw new NullReferenceException("No Attribute reference found");
                }

                AttributeType attributeType = attributeItem.AttributeType;
                if (_attributes.ContainsKey(attributeType))
                {
                    throw new Exception($"Attribute with AttributeType id = '{attributeType.Id}' already exists");
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

        public RuntimeAttribute Get(AttributeType attributeType) => _attributes[attributeType];
        public bool Contains(AttributeType attributeType) => _attributes.ContainsKey(attributeType);

        public IEnumerator<RuntimeAttribute> GetEnumerator() => _attributes.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}