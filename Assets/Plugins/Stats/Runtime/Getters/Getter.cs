using System;
using System.Linq;
using UnityEngine;

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
        [SerializeReference] private object _property;
#if UNITY_EDITOR
        private Type _defaultEditorPropertyType;
#endif

        public IGetType<TValue> Property
        {
            get => _property as IGetType<TValue>;
            set => _property = value;
        }

        public Getter(IGetType<TValue> defaultValue)
        {
            _property = defaultValue;
        }

        public Getter()
        {
        }

        public virtual TValue Get() => Property != null ? Property.Get() : default;

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
                Property = (IGetType<TValue>)Activator.CreateInstance(type);
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