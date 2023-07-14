using System;
using System.Collections;
using System.Reflection;
using UnityEditor;

namespace Stats.Editor
{
    public static class StatsEditorHelper
    {
        private const BindingFlags Flags = BindingFlags.Default | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
        
        public static string GetStatItemHeader (StatItem statItem, SerializedProperty property)
        {
            var itemName = GetItemName<StatItem>(property);
            return statItem.StatType == null ? itemName : $"{itemName}: {statItem.Base}";
        }

        public static string GetRuntimeStatHeader(IRuntimeStat runtimeStat)
        {
            string name = runtimeStat.StatType.name;
            float value = runtimeStat.Value;
            float modifiersValue = runtimeStat.ModifiersValue;
            string modifiersValueString = modifiersValue != 0 ? $"<>{(modifiersValue > 0 ? "+" : "-")}{modifiersValue}" : "";
            string modifiersText = modifiersValue > 0
                ? $"<color=green>{modifiersValueString}</color>"
                : $"<color=red>{modifiersValueString}</color>";

            return $"{name}: {value - modifiersValue} {modifiersText}";
        }
        
        public static string GetAttributeItemHeader(AttributeItem attributeItem, SerializedProperty property)
        {
            var startPercent = attributeItem.StartPercent;
            var itemName = GetItemName<AttributeItem>(property);
            
            return attributeItem.AttributeType == null ? itemName : $"{itemName}: {Math.Round(startPercent * 100, 1)}%";
        }

        public static string GetRuntimeAttributeHeader(IRuntimeAttribute runtimeAttribute)
        {
            var name = runtimeAttribute.AttributeType.name;
            var value = runtimeAttribute.Value;
            var maxValue = runtimeAttribute.MaxValue;
            
            return $"{name}: {value} / {maxValue}";
        }
        
        public static T GetValue<T>(SerializedProperty property)
        {
            object obj = property.serializedObject.targetObject;

            string[] path = property.propertyPath.Split('.');
            for ( var i = 0; i < path.Length; i++)
            {
                if (path[i] == "Array")
                {
                    if (obj is IList a) obj = a[int.Parse(path[i+1][^2].ToString())];
                    continue;
                }
                
                Type type = obj.GetType();
                
                FieldInfo field = null;
                while (field == null)
                {
                    field = type.GetField(path[i], Flags);
                    
                    if (field == null)
                    {
                        type = type.BaseType;
                        if (type == null)
                            break;
                    }
                }

                if (field != null) obj = field.GetValue(obj);
            }
            return (T)obj;
        }

        private static string GetItemName<T>(SerializedProperty property)
        {
            if (property.name == "data" && property.depth > 0 && property.displayName.Contains("Element"))
            {
                return GetItemTypeName<T>(property);
            }
            
            return property.displayName;
        }

        private static string GetItemTypeName<T>(SerializedProperty property)
        {
            var label = "";
            
            if (typeof(T) == typeof(AttributeItem))
            {
                AttributeType type = GetValue<AttributeItem>(property).AttributeType;

                label = type == null ? "<color=yellow>Select Attribute</color>" : type.name;
            }

            if (typeof(T) == typeof(StatItem))
            {
                StatType type = GetValue<StatItem>(property).StatType;

                label = type == null ? "<color=yellow>Select Stat</color>" : type.name;
            }

            return label;
        }
    }
}