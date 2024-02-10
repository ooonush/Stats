using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Stats
{
    public abstract class RuntimeAttributesBase : IRuntimeAttributes
    {
        private static readonly MethodInfo AddAttributeGenericMethodDefinition;

        static RuntimeAttributesBase()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            Type[] types = { typeof(IAttribute<>) };
            MethodInfo methodInfo = typeof(RuntimeAttributesBase).GetGenericMethod(nameof(AddAttribute), flags, types);
            AddAttributeGenericMethodDefinition = methodInfo!.GetGenericMethodDefinition();
        }
        
        protected readonly ITraits Traits;
        private readonly Dictionary<string, IRuntimeAttribute> _attributes = new();

        public int Count => _attributes.Count;

        protected RuntimeAttributesBase(ITraits traits)
        {
            Traits = traits;
        }

        private void AddAttribute<TNumber>(IAttribute<TNumber> attribute) where TNumber : struct, IStatNumber<TNumber>
        {
            IRuntimeAttribute runtimeAttribute = CreateRuntimeAttribute(attribute);
            
            if (!_attributes.TryAdd(runtimeAttribute.AttributeId, runtimeAttribute))
            {
                throw new Exception($"Stat with id \"{runtimeAttribute.AttributeId}\" already exists");
            }
        }

        protected abstract IRuntimeAttribute CreateRuntimeAttribute<TNumber>(IAttribute<TNumber> stat)
            where TNumber : IStatNumber<TNumber>;

        void IRuntimeAttributes.SyncWithTraitsClass(ITraitsClass traitsClass)
        {
            Clear();

            foreach (IAttribute attribute in traitsClass.Attributes)
            {
                foreach (Type attributeInterface in attribute.GetType().GetInterfaces())
                {
                    if (!attributeInterface.IsGenericType) continue;
                    if (attributeInterface.GetGenericTypeDefinition() != typeof(IAttribute<>)) continue;
                    
                    Type genericAttributeNumberType = attributeInterface.GenericTypeArguments[0];
                    
                    MethodInfo methodInfo = AddAttributeGenericMethodDefinition.MakeGenericMethod(genericAttributeNumberType);
                    methodInfo.Invoke(this, new object[] { attribute });
                }
            }
        }

        protected void Clear()
        {
            _attributes.Clear();
        }

        public bool Contains(AttributeId attributeId)
        {
            return _attributes.ContainsKey(attributeId);
        }

        public IRuntimeAttribute<TNumber> Get<TNumber>(AttributeId<TNumber> attributeId) where TNumber : IStatNumber<TNumber>
        {
            return (IRuntimeAttribute<TNumber>)Get((string)attributeId);
        }

        public IRuntimeAttribute Get(AttributeId attributeId)
        {
            if (attributeId == default)
            {
                throw new ArgumentNullException(nameof(attributeId));
            }
            
            try
            {
                return _attributes[attributeId];
            }
            catch (Exception exception)
            {
                throw new ArgumentException("AttributeType not found in RuntimeAttributes", nameof(attributeId),
                    exception);
            }
        }

        public bool Contains<TNumber>(AttributeId<TNumber> attributeId) where TNumber : IStatNumber<TNumber>
        {
            return Contains((string)attributeId);
        }

        public IEnumerator<IRuntimeAttribute> GetEnumerator() => _attributes.Values.GetEnumerator();

        void IRuntimeAttributes.InitializeStartValues()
        {
            foreach (IRuntimeAttribute runtimeAttribute in _attributes.Values)
            {
                runtimeAttribute.InitializeStartValues();
            }
        }

        IRuntimeAttribute<TNumber> IRuntimeAttributes.Get<TNumber>(AttributeId<TNumber> attributeId) => Get(attributeId);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}