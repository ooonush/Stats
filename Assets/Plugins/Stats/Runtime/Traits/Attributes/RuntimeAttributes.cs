using System;
using System.Collections;
using System.Collections.Generic;

namespace Stats
{
    [Serializable]
    public sealed class RuntimeAttributes : IRuntimeAttributes
    {
        private readonly Traits _traits;
        private readonly Dictionary<string, IRuntimeAttributeBase> _attributes = new();

        public int Count => _attributes.Count;

        internal RuntimeAttributes(Traits traits)
        {
            _traits = traits;
        }

        public void SyncWithTraitsClass(ITraitsClass traitsClass)
        {
            _attributes.Clear();
            
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
                        
                        _attributes[attributeId] = (IRuntimeAttributeBase)genericRuntimeAttribute;
                    }
                }
            }
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

        IRuntimeAttribute<TNumber> IRuntimeAttributes.Get<TNumber>(AttributeId<TNumber> attributeId) => Get(attributeId);

        public IEnumerator<IRuntimeAttribute> GetEnumerator() => _attributes.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}