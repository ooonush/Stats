using System;
using System.Collections.Generic;
using System.Linq;

namespace Stats
{
    [Serializable]
    public sealed class RuntimeAttributes : IRuntimeAttributes
    {
        private readonly Traits _traits;
        private readonly Dictionary<string, RuntimeAttribute> _attributes = new();

        internal event Action OnValueChanged;

        internal RuntimeAttributes(Traits traits)
        {
            _traits = traits;
        }

        public void SyncWithTraitsClass(ITraitsClass traitsClass)
        {
            ClearAttributes();

            foreach ((string attributeId, object attribute) in traitsClass.AttributeItems)
            {
                foreach (Type attributeInterface in attribute.GetType().GetInterfaces())
                {
                    if (attributeInterface.IsGenericType && attributeInterface.GetGenericTypeDefinition() == typeof(IAttribute<>))
                    {
                        Type genericAttributeNumberType = attributeInterface.GenericTypeArguments[0];
                        Type runtimeAttributeType = typeof(RuntimeAttribute<>).MakeGenericType(genericAttributeNumberType);

                        object genericRuntimeAttribute = Activator.CreateInstance(runtimeAttributeType, _traits, attribute);

                        if (_attributes.ContainsKey(attributeId))
                        {
                            throw new Exception($"Stat with id \"{attributeId}\" already exists");
                        }

                        AddRuntimeAttribute(attributeId, (RuntimeAttribute)genericRuntimeAttribute);
                    }
                }
            }
        }

        private void AddRuntimeAttribute(string attributeId, RuntimeAttribute runtimeAttribute)
        {
            _attributes[attributeId] = runtimeAttribute;
            runtimeAttribute.OnValueChanged += OnValueChanged;
        }

        private void RemoveRuntimeAttribute(string attributeId)
        {
            RuntimeAttribute runtimeAttribute = _attributes[attributeId];
            runtimeAttribute.OnValueChanged -= OnValueChanged;
            _attributes.Remove(attributeId);
        }

        private void ClearAttributes()
        {
            foreach (string attributeId in _attributes.Keys.ToArray())
            {
                RemoveRuntimeAttribute(attributeId);
            }
        }

        IRuntimeAttribute<TNumber> IRuntimeAttributes.Get<TNumber>(AttributeId<TNumber> attributeId)
        {
            return Get(attributeId);
        }

        public RuntimeAttribute<TNumber> Get<TNumber>(AttributeId<TNumber> attributeId) where TNumber : IStatNumber<TNumber>
        {
            if (attributeId == default)
            {
                throw new ArgumentNullException(nameof(attributeId));
            }
            
            try
            {
                return (RuntimeAttribute<TNumber>)_attributes[attributeId];
            }
            catch (Exception exception)
            {
                throw new ArgumentException("AttributeType not found in RuntimeAttributes", nameof(attributeId),
                    exception);
            }
        }

        public bool Contains<TNumber>(AttributeId<TNumber> attributeType) where TNumber : IStatNumber<TNumber>
        {
            return _attributes.ContainsKey(attributeType);
        }

        public IEnumerator<RuntimeAttribute> GetEnumerator() => _attributes.Values.GetEnumerator();
    }
}