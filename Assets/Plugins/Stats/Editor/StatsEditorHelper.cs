using System;
using System.Collections;
using System.Reflection;
using UnityEditor;

namespace Stats.Editor
{
    public static class StatsEditorHelper
    {
        public static string GetStatItemHeader (StatItem statItem, string label)
        {
            return $"{label}: {statItem.Base}";
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
        
        public static string GetAttributeItemHeader(AttributeItem attributeItem, string label)
        {
            float startPercent = attributeItem.StartPercent;
            
            return $"{label}: {Math.Round(startPercent * 100, 1)}%";
        }

        public static string GetRuntimeAttributeHeader(IRuntimeAttribute runtimeAttribute)
        {
            string name = runtimeAttribute.AttributeType.name;
            float value = runtimeAttribute.Value;
            float maxValue = runtimeAttribute.MaxValue;
            
            return $"{name}: {value} / {maxValue}";
        }
        
        public static T GetValue<T>(SerializedProperty property)
        {
            BindingFlags bindingFlags = BindingFlags.Default | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
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
                    field = type.GetField(path[i], bindingFlags);
                    
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

        public static string GetLabel<T>(SerializedProperty property)
        {
            if (property.name == "data" && property.depth > 0 && property.displayName.Contains("Element"))
            {
                return GetTypeName<T>(property);
            }
            
            return property.displayName;
        }

        private static string GetTypeName<T>(SerializedProperty property)
        {
            if (typeof(T) == typeof(AttributeItem))
            {
                return GetValue<AttributeItem>(property).AttributeType.name;
            }

            return typeof(T) == typeof(StatItem) ? GetValue<StatItem>(property).StatType.name : "";
        }
    }
}