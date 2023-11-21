using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Stats
{
    /// <summary>
    /// The base class for all traits classes.
    /// </summary>
    // TODO: Replace it with the source generator.
    public abstract class TraitsClassAsset : IdScriptableObject, ITraitsClass
    {
        private Dictionary<string, object> _statItems;
        private Dictionary<string, object> _attributeItems;

        /// <summary>
        /// All Stats in this TraitsClass.
        /// </summary>
        public IReadOnlyDictionary<string, object> StatItems
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
        public IReadOnlyDictionary<string, object> AttributeItems
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

        private static readonly Type StatType = typeof(IStat<>);
        private static readonly Type AttributeType = typeof(IAttribute<>);
        private static readonly Type StatItemType = typeof(StatItem<>);
        private static readonly Type AttributeItemType = typeof(AttributeItem<>);
        private static readonly Type EnumerableType = typeof(IEnumerable<>); 
        private static readonly Type StatEnumerableType; 
        private static readonly Type AttributeEnumerableType; 
        private static readonly Type StatItemEnumerableType; 
        private static readonly Type AttributeItemEnumerableType;
        
        static TraitsClassAsset()
        {
            StatEnumerableType = EnumerableType.MakeGenericType(typeof(IStat<>));
            AttributeEnumerableType = EnumerableType.MakeGenericType(typeof(IAttribute<>));
            StatItemEnumerableType = EnumerableType.MakeGenericType(StatItemType);
            AttributeItemEnumerableType = EnumerableType.MakeGenericType(AttributeItemType);
        }

        protected override void OnValidation() => InitializeItems();

        private void InitializeItems()
        {
            _statItems = new Dictionary<string, object>();
            _attributeItems = new Dictionary<string, object>();

            foreach (FieldInfo fieldInfo in GetFields(GetType()))
            {
                if (fieldInfo.Name == nameof(_statItems)) continue;
                if (fieldInfo.Name == nameof(_attributeItems)) continue;
                if (fieldInfo.Name == "_id") continue;

                object fieldValue = fieldInfo.GetValue(this);

                if (fieldValue == null || RegisterFieldByType(fieldInfo, fieldValue)) continue;
                if (!fieldInfo.IsDefined(typeof(TraitsClassConfigAttribute), true)) continue;

                foreach (FieldInfo field in GetFields(fieldValue.GetType()))
                {
                    RegisterFieldByType(field, field.GetValue(fieldValue));
                }
            }
        }

        private bool RegisterFieldByType(FieldInfo fieldInfo, object fieldValue)
        {
            Type fieldType = fieldInfo.FieldType;
            
            if (fieldType.IsGenericType)
            {
                Type genericTypeDefinition = fieldType.GetGenericTypeDefinition();
                
                if (StatType == genericTypeDefinition)
                {
                    AddStat(fieldValue);
                    return true;
                }
                if (AttributeType == genericTypeDefinition)
                {
                    AddAttribute(fieldValue);
                    return true;
                }
                if (StatItemType == genericTypeDefinition)
                {
                    AddStatItem(fieldValue);
                    return true;
                }
                if (AttributeItemType == genericTypeDefinition)
                {
                    AddAttributeItem(fieldValue);
                    return true;
                }
            }

            foreach (Type fieldInterface in fieldType.GetInterfaces())
            {
                if (!fieldInterface.IsGenericType) continue;
                Type genericTypeDefinition = fieldInterface.GetGenericTypeDefinition();
                
                if (genericTypeDefinition != EnumerableType) continue;
                
                if (fieldInterface.GenericTypeArguments[0].IsGenericType)
                {
                    genericTypeDefinition = genericTypeDefinition.MakeGenericType(fieldInterface.GenericTypeArguments[0].GetGenericTypeDefinition());
                }
                
                if (StatEnumerableType == genericTypeDefinition)
                {
                    foreach (object stat in (IEnumerable)fieldValue)
                    {
                        AddStat(stat);
                    }
                    return true;
                }
                if (AttributeEnumerableType == genericTypeDefinition)
                {
                    foreach (object attribute in (IEnumerable)fieldValue)
                    {
                        AddAttribute(attribute);
                    }
                    return true;
                }
                if (StatItemEnumerableType == genericTypeDefinition)
                {
                    foreach (object statItem in (IEnumerable)fieldValue)
                    { 
                        AddStatItem(statItem);
                    }
                    return true;
                }
                if (AttributeItemEnumerableType == genericTypeDefinition)
                {
                    foreach (object attributeItem in (IEnumerable)fieldValue)
                    {
                        AddAttributeItem(attributeItem);
                    }
                    return true;
                }
            }
            
            return false;
        }

        private const BindingFlags Flags = BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

        private void AddAttribute(object attribute)
        {
            if (attribute == null) return;
            
            object attributeId = attribute.GetType().GetProperty("AttributeId", Flags)!.GetValue(attribute);
            
            if (attributeId == null) return;
            
            var attributeIdString = attributeId.ToString();
            
            if (string.IsNullOrEmpty(attributeIdString)) return;
            
            _attributeItems.Add(attributeIdString, attribute);
            
            AddStat(attribute.GetType().GetProperty("MaxValueStat", Flags)!.GetValue(attribute));
        }

        private void AddStat(object stat)
        {
            if (stat == null) return;
            
            object statId = stat.GetType().GetProperty("StatId", Flags)!.GetValue(stat);
            
            if (statId == null) return;
            
            var statIdString = statId.ToString();
            
            if (string.IsNullOrEmpty(statIdString)) return;
            
            _statItems.Add(statIdString, stat);
        }

        private void AddStatItem(object statItem)
        {
            Type genericStatItemType = StatItemType.MakeGenericType(statItem.GetType().GenericTypeArguments[0]);
            PropertyInfo statPropertyInfo = genericStatItemType.GetProperty("Stat");
            AddStat(statPropertyInfo!.GetValue(statItem));
        }

        private void AddAttributeItem(object attributeItem)
        {
            Type genericAttributeItemType = AttributeItemType.MakeGenericType(attributeItem.GetType().GenericTypeArguments[0]);
            PropertyInfo attributePropertyInfo = genericAttributeItemType.GetProperty("Attribute");
            AddAttribute(attributePropertyInfo!.GetValue(attributeItem));
        }

        private static IEnumerable<FieldInfo> GetFields(Type type)
        {
            if (type == null)
            {
                return Array.Empty<FieldInfo>();
            }

            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly;
            var fields = new List<FieldInfo>(type.GetFields(flags));

            if (type.BaseType != null)
            {
                fields.AddRange(GetFields(type.BaseType));
            }

            return fields;
        }
    }
}