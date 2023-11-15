using System;
using System.Reflection;
using AInspector;
using UnityEditor;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    [CustomPropertyDrawer(typeof(Getter<>), true)]
    public sealed class GetterDrawer : PropertyDrawer
    {
        private PropertyTypeSelectionDropdown _typeSelectionDropdown;
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            const string propertyName = "_property";
            SerializedProperty propertyItem = property.FindPropertyRelative(propertyName);
            Type targetType = fieldInfo.FieldType.GetProperty("Property", Flags)!.PropertyType;
            
            var dropdown = new PropertyTypeSelectionDropdown(propertyItem, targetType, preferredLabel);
            dropdown.DropdownField.AddToClassList("unity-base-field__inspector-field");
            dropdown.DropdownField.AddToClassList("unity-base-field__aligned");

            return dropdown;
        }
    }
}