using System;
using System.Collections.Generic;
using System.Reflection;

namespace Stats
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TraitsClassConfigAttribute : Attribute
    {
        
    }

    /// <summary>
    /// The base class for all traits classes.
    /// </summary>
    public abstract class TraitsClassBase : IdScriptableObject
    {
        private static readonly Dictionary<Type, FieldInfo[]> Fields = new();

        private HashSet<StatItem> _statItems;
        private HashSet<AttributeItem> _attributeItems;

        /// <summary>
        /// All Stats in this TraitsClass.
        /// </summary>
        public IEnumerable<StatItem> StatItems
        {
            get
            {
                if (_statItems == null)
                {
                    InitializeItems();
                }
                return _statItems;
            }
        }

        /// <summary>
        /// All Attributes in this TraitsClass.
        /// </summary>
        public IEnumerable<AttributeItem> AttributeItems
        {
            get
            {
                if (_attributeItems == null)
                {
                    InitializeItems();
                }
                return _attributeItems;
            }
        }

        protected override void OnValidation()
        {
            InitializeItems();
        }

        private void InitializeItems()
        {
            _statItems = new HashSet<StatItem>();
            _attributeItems = new HashSet<AttributeItem>();

            foreach (FieldInfo fieldInfo in GetFieldsByType(GetType()))
            {
                object fieldValue = fieldInfo.GetValue(this);

                if (fieldValue == null || RegisterFieldByType(fieldValue)) continue;
                if (!fieldInfo.IsDefined(typeof(TraitsClassConfigAttribute), true)) continue;

                foreach (FieldInfo field in GetFieldsByType(fieldValue.GetType()))
                {
                    RegisterFieldByType(field.GetValue(fieldValue));
                }
            }
        }

        private bool RegisterFieldByType(object fieldValue)
        {
            switch (fieldValue)
            {
                case IEnumerable<StatItem> fieldStatItems:
                    foreach (StatItem statItem in fieldStatItems)
                    {
                        AddStat(statItem);
                    }
                    return true;
                case IEnumerable<AttributeItem> fieldAttributeItems:
                    foreach (AttributeItem attributeItem in fieldAttributeItems)
                    {
                        AddAttribute(attributeItem);
                    }
                    return true;
                case StatItem fieldStatItem:
                    AddStat(fieldStatItem);
                    return true;
                case AttributeItem fieldAttributeItem:
                    AddAttribute(fieldAttributeItem);
                    return true;
            }

            return false;
        }

        private void AddAttribute(AttributeItem attributeItem) => _attributeItems.Add(attributeItem);

        private void AddStat(StatItem statItem) => _statItems.Add(statItem);

        private static IEnumerable<FieldInfo> GetFieldsByType(Type type)
        {
            if (type == null)
            {
                return Array.Empty<FieldInfo>();
            }

            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly;
            var fields = new List<FieldInfo>(type.GetFields(flags));

            if (type.BaseType != null)
            {
                fields.AddRange(GetFieldsByType(type.BaseType));
            }

            return fields;
        }
    }
}