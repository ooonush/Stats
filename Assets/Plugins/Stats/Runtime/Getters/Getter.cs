using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Stats
{
    [Serializable]
    public class Getter<TValue>
#if UNITY_EDITOR
        : ISerializationCallbackReceiver
#endif
    {
        [SerializeReference] protected object Property;
#if UNITY_EDITOR
        private Type _defaultEditorPropertyType;
#endif

        public virtual TValue Value
        {
            get
            {
                if (Property is ObjectValueGetType objectValueGetType)
                {
                    return objectValueGetType.Value is TValue value ? value : default;
                }
                return Property is IGetType<TValue> getType ? getType.Get() : default;
            }
        }

        public Getter(IGetType<TValue> defaultValue)
        {
            Property = defaultValue;
        }

        public Getter(TValue value)
        {
            if (value is Object asset)
            {
                Property = new AssetValueGetType(asset);
            }
            else
            {
                Property = new ObjectValueGetType(value);
            }
        }

        public Getter()
        {
        }

        public override string ToString() => Property?.ToString() ?? "(none)";

        protected void SetDefaultEditorPropertyType<T>() where T : IGetType<TValue>
        {
#if UNITY_EDITOR
            _defaultEditorPropertyType = typeof(T);
#endif
        }

#if UNITY_EDITOR
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (Property != null || _defaultEditorPropertyType == null) return;
            
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom(_defaultEditorPropertyType);
            foreach (Type type in types.Where(IsFinalNonGenericAssignableType))
            {
                Property = Activator.CreateInstance(type);
                return;
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }

        private static bool IsFinalNonGenericAssignableType(Type type)
        {
            return !type.IsAbstract && !type.IsInterface && !type.IsGenericType && type.GetConstructors().Any(c => c.IsPublic && c.GetParameters().Length == 0 && !c.IsStatic);
        }
#endif
    }
}