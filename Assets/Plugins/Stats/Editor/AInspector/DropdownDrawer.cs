using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace AInspector
{
    [CustomPropertyDrawer(typeof(DropdownAttribute))]
    public class DropdownDrawer : PropertyDrawer
    {
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.ManagedReference)
            {
                return new PropertyField(property);
            }
            
            var dropdown = new PropertyTypeSelectionDropdown(property, GetTargetType(property), preferredLabel, ((DropdownAttribute)attribute).IncludeNull);
            dropdown.DropdownField.AddToClassList("unity-base-field__inspector-field");
            dropdown.DropdownField.AddToClassList("unity-base-field__aligned");
            return dropdown;
        }
        
        private Type GetTargetType(SerializedProperty property)
        {
            var genericDropdownAttribute = fieldInfo.GetCustomAttribute<GenericDropdownAttribute>();
            if (genericDropdownAttribute == null)
            {
                return TypeUtils.ExtractTypeFromString(property.managedReferenceFieldTypename);
            }
            Type propertyType = fieldInfo.ReflectedType;
            return GetPropertyIncludeBase(propertyType, genericDropdownAttribute.GenericGetterName, Flags).PropertyType;
        }

        private static PropertyInfo GetPropertyIncludeBase(Type type, string propertyName, BindingFlags bindingFlags)
        {
            Type baseType = type;
            while (baseType != null)
            {
                PropertyInfo property = baseType.GetProperty(propertyName, bindingFlags);
                baseType = baseType.BaseType;
                if (property != null)
                {
                    return property;
                }
            }
            
            return default;
        }
    }
}