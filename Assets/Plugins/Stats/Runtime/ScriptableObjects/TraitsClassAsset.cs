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
        private List<IStat> _statItems;
        private List<IAttribute> _attributeItems;

        /// <summary>
        /// All Stats in this TraitsClass.
        /// </summary>
        public IReadOnlyList<IStat> Stats
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
        public IReadOnlyList<IAttribute> Attributes
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
        private static readonly Type StatItemType = typeof(StatItem);
        private static readonly Type AttributeItemType = typeof(AttributeItem);
        private static readonly Type GenericStatItemType = typeof(StatItem<>);
        private static readonly Type GenericAttributeItemType = typeof(AttributeItem<>);
        private static readonly Type EnumerableType = typeof(IEnumerable<>); 
        private static readonly Type StatEnumerableType; 
        private static readonly Type AttributeEnumerableType; 
        private static readonly Type StatItemEnumerableType; 
        private static readonly Type AttributeItemEnumerableType;
        private static readonly Type GenericStatItemEnumerableType; 
        private static readonly Type GenericAttributeItemEnumerableType;
        
        static TraitsClassAsset()
        {
            StatEnumerableType = EnumerableType.MakeGenericType(typeof(IStat<>));
            AttributeEnumerableType = EnumerableType.MakeGenericType(typeof(IAttribute<>));
            StatItemEnumerableType = EnumerableType.MakeGenericType(StatItemType);
            AttributeItemEnumerableType = EnumerableType.MakeGenericType(AttributeItemType);
            
            GenericStatItemEnumerableType = EnumerableType.MakeGenericType(GenericStatItemType);
            GenericAttributeItemEnumerableType = EnumerableType.MakeGenericType(GenericAttributeItemType);
        }

        protected override void OnValidation() => InitializeItems();

        private void InitializeItems()
        {
            _statItems = new List<IStat>();
            _attributeItems = new List<IAttribute>();

            foreach (FieldInfo fieldInfo in GetFields(GetType()))
            {
                switch (fieldInfo.Name)
                {
                    case nameof(_statItems):
                    case nameof(_attributeItems):
                    case "_id":
                        continue;
                }

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

            if (StatItemType == fieldType)
            {
                AddStatItem(fieldValue);
                return true;
            }
            if (AttributeItemType == fieldType)
            {
                AddAttributeItem(fieldValue);
                return true;
            }

            if (fieldType.IsGenericType)
            {
                Type genericTypeDefinition = fieldType.GetGenericTypeDefinition();
                
                if (StatType == genericTypeDefinition)
                {
                    AddStat((IStat)fieldValue);
                    return true;
                }
                if (AttributeType == genericTypeDefinition)
                {
                    AddAttribute((IAttribute)fieldValue);
                    return true;
                }
                if (GenericStatItemType == genericTypeDefinition)
                {
                    AddStatItem(fieldValue);
                    return true;
                }
                if (GenericAttributeItemType == genericTypeDefinition)
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
                        AddStat((IStat)stat);
                    }
                    return true;
                }
                if (AttributeEnumerableType == genericTypeDefinition)
                {
                    foreach (object attribute in (IEnumerable)fieldValue)
                    {
                        AddAttribute((IAttribute)attribute);
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
                if (GenericStatItemEnumerableType == genericTypeDefinition)
                {
                    foreach (object statItem in (IEnumerable)fieldValue)
                    { 
                        AddStatItem(statItem);
                    }
                    return true;
                }
                if (GenericAttributeItemEnumerableType == genericTypeDefinition)
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

        private void AddAttribute(IAttribute attribute)
        {
            if (attribute == null) return;
            
            _attributeItems.Add(attribute);
            AddStat(attribute.MaxValueStat);
        }

        private void AddStat(IStat stat)
        {
            if (stat == null) return;
            
            _statItems.Add(stat);
        }

        private void AddStatItem(object statItem)
        {
            if (statItem.GetType() == StatItemType)
            {
                AddStat(((StatItem)statItem).Stat);
            }
            else
            {
                Type genericStatItemType = GenericStatItemType.MakeGenericType(statItem.GetType().GenericTypeArguments[0]);
                PropertyInfo statPropertyInfo = genericStatItemType.GetProperty("Stat");
                AddStat((IStat)statPropertyInfo!.GetValue(statItem));
            }
        }

        private void AddAttributeItem(object attributeItem)
        {
            if (attributeItem.GetType() == AttributeItemType)
            {
                AddAttribute(((AttributeItem)attributeItem).Attribute);
            }
            else
            {
                Type genericAttributeItemType = GenericAttributeItemType.MakeGenericType(attributeItem.GetType().GenericTypeArguments[0]);
                PropertyInfo attributePropertyInfo = genericAttributeItemType.GetProperty("Attribute");
                AddAttribute((IAttribute)attributePropertyInfo!.GetValue(attributeItem));
            }
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