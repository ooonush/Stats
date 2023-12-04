using System;
using AInspector;
using UnityEditor;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    [CustomPropertyDrawer(typeof(Getter<>), true)]
    public sealed class GetterDrawer : PropertyDrawer
    {
        private PropertyTypeSelectionDropdown _typeSelectionDropdown;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            const string propertyName = "Property";
            SerializedProperty propertyItem = property.FindPropertyRelative(propertyName);
            
            Type genericType = GetGenericGetType(property.GetValue().GetType());
            Type targetType = typeof(IGetType<>).MakeGenericType(genericType);
            
            PropertyTypeSelectionDropdown dropdown = CreateDropdown(propertyItem, new[] { targetType });
            
            return dropdown;
        }

        private PropertyTypeSelectionDropdown CreateDropdown(SerializedProperty propertyItem, Type[] types)
        {
            var dropdown = new PropertyTypeSelectionDropdown(propertyItem, types, preferredLabel);
            dropdown.DropdownField.AddToClassList("unity-base-field__inspector-field");
            dropdown.DropdownField.AddToClassList("unity-base-field__aligned");
            return dropdown;
        }

        private static Type GetGenericGetType(Type type)
        {
            while (type != typeof(object) && (!type!.IsGenericType || type.GetGenericTypeDefinition() != typeof(Getter<>)))
            {
                type = type.BaseType;
            }
            
            Type genericType = type.GetGenericArguments()[0];
            return genericType;
        }
    }
}