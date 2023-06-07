using System;
using System.Collections.Generic;
using System.Reflection;

namespace Stats
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TraitsClassConfigAttribute : System.Attribute
    {
        
    }

    public abstract class TraitsClassBase : IdScriptableObject
    {
        private static readonly Dictionary<Type, FieldInfo[]> Fields = new Dictionary<Type, FieldInfo[]>();

        private HashSet<StatItem> _statItems;
        private HashSet<AttributeItem> _attributeItems;

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

        private void AddAttribute(AttributeItem attributeItem)
        {
            _attributeItems.Add(attributeItem);
        }

        private void AddStat(StatItem statItem)
        {
            _statItems.Add(statItem);
        }

        private static IEnumerable<FieldInfo> GetFieldsByType(Type type)
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            if (Fields.TryGetValue(type, out var byType))
            {
                return byType;
            }

            Fields.Add(type, type.GetFields(bindingFlags));
            return Fields[type];
        }
    }
}