using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AInspector
{
    public class PropertyTypeSelectionDropdown : BindableElement
    {
        private const string NullName = "null";

        public readonly DropdownField DropdownField;

        private readonly Button _selectTypeButton;
        private readonly bool _allowNull;
        private readonly Dictionary<string, Type> _typeByName = new();
        private readonly Dictionary<Type, string> _nameByType = new();
        private string _value;
        private readonly PropertyField _propertyField;

        public PropertyTypeSelectionDropdown(SerializedProperty property, Type[] types, string label, bool allowNull = false)
        {
            if (property.propertyType != SerializedPropertyType.ManagedReference)
            {
                Add(new Label("Type Selection Dropdown only managed references are supported"));
                return;
            }
            _allowNull = allowNull;
            
            _propertyField = new PropertyField(property);
            _propertyField.RegisterCallback<GeometryChangedEvent>(GeometryChangedEvent);
            
            DropdownField = new DropdownField(label);
            DropdownField.style.marginTop = 0;
            DropdownField.RegisterValueChangedCallback(SetPickup);
            
            _value = DropdownField.value;
            
            Add(_propertyField);
            Add(DropdownField);
            
            style.marginTop = 1;
            
            InitializeDropdownTypes(property, types);
            
            this.Bind(property.serializedObject);
            return;
            
            void SetPickup(ChangeEvent<string> evt)
            {
                if (_value == DropdownField.value) return;
                
                _value = DropdownField.value;
                
                Type type = _value == NullName ? null : _typeByName[_value];
                object newObject = CreateInstanceByType(type);
                ApplyValueToProperty(newObject, property);
                _propertyField.BindProperty(property);
                _propertyField.RegisterCallback<GeometryChangedEvent>(GeometryChangedEvent);
            }
            
            void GeometryChangedEvent(GeometryChangedEvent evt)
            {
                _propertyField.UnregisterCallback<GeometryChangedEvent>(GeometryChangedEvent);
                UpdateDropdown();
            }

            void UpdateDropdown()
            {
                Rect dropdownRect = DropdownField.worldBound;
                dropdownRect.height = EditorGUIUtility.singleLineHeight;
                dropdownRect.y = _propertyField.worldBound.y;
                
                if (_propertyField.localBound.height == 0)
                {
                    _propertyField.RegisterCallback<GeometryChangedEvent>(GeometryChangedEvent);
                    return;
                }
                
                if (!Get<Foldout>(_propertyField).Any(foldout => foldout.worldBound.Overlaps(dropdownRect)))
                {
                    _propertyField.label = property.GetChildren().Count == 1 ? " " : label;
                    _propertyField.PlaceInFront(DropdownField);
                    EnableDropdownLabelPicking(DropdownField);
                }
                else
                {
                    _propertyField.label = label;
                    _propertyField.PlaceBehind(DropdownField);
                    DisableDropdownLabelPicking(DropdownField);
                }
                _propertyField.BindProperty(property);
            }
        }

        private void InitializeDropdownTypes(SerializedProperty property, IEnumerable<Type> types)
        {
            foreach (Type targetType in types)
            {
                foreach (Type type in GetAssignableTypes(targetType))
                {
                    string typeName = GetTypeName(type);
                    DropdownField.choices.Add(typeName);
                    if (type != null)
                    {
                        _typeByName[typeName] = type;
                        _nameByType[type] = typeName;
                    }
                }
            }
            
            if (property.managedReferenceValue == null) return;
            
            Type currentType = property.managedReferenceValue.GetType();
            if (_nameByType.TryGetValue(currentType, out string currentTypeName))
            {
                DropdownField.value = currentTypeName;
            }
            else
            {
                DropdownField.value = GetTypeName(currentType);
            }
        }

        private static IEnumerable<T> Get<T>(VisualElement visualElement)
        {
            foreach (VisualElement child in visualElement.hierarchy.Children())
            {
                if (child is T target)
                {
                    yield return target;
                }
                
                foreach (T targetChild in Get<T>(child))
                {
                    yield return targetChild;
                }
            }
        }

        private static void DisableDropdownLabelPicking<T>(BaseField<T> field)
        {
            field.labelElement.style.visibility = new StyleEnum<Visibility>(Visibility.Hidden);
            field.style.position = new StyleEnum<Position>(Position.Absolute);
            field.style.left = 0;
            field.style.right = 0;
            field.pickingMode = PickingMode.Ignore;
            field.labelElement.pickingMode = PickingMode.Ignore;
            field.Q(className: "unity-popup-field__input").pickingMode = PickingMode.Position;
        }

        private static void EnableDropdownLabelPicking<T>(BaseField<T> field)
        {
            field.labelElement.style.visibility = new StyleEnum<Visibility>(Visibility.Visible);
            field.style.position = new StyleEnum<Position>(Position.Relative);
            field.style.left = new StyleLength(StyleKeyword.Auto);
            field.style.right = new StyleLength(StyleKeyword.Auto);
            field.pickingMode = PickingMode.Position;
            field.labelElement.pickingMode = PickingMode.Position;
            field.Q(className: "unity-popup-field__input").pickingMode = PickingMode.Ignore;
        }

        private static string GetTypeName(Type type)
        {
            if (type == null) return NullName;
            
            var nameAttribute = type.GetCustomAttribute<DropdownNameAttribute>();
            return nameAttribute != null ? nameAttribute.Name : ObjectNames.NicifyVariableName(type.Name);
        }

        private List<Type> GetAssignableTypes(Type targetType)
        {
            var nonUnityTypes = new List<Type>();
            if (_allowNull)
            {
                nonUnityTypes.Add(null);
            }
            if (IsCorrectType(targetType))
            {
                nonUnityTypes.Add(targetType);
            }
            nonUnityTypes.AddRange(TypeCache.GetTypesDerivedFrom(targetType)
                .Where(IsCorrectType));
            return nonUnityTypes;

            bool IsCorrectType(Type type)
            {
                return TypeUtils.IsFinalNonGenericAssignableType(type) && !type.IsSubclassOf(typeof(UnityEngine.Object));
            }
        }

        private static object CreateInstanceByType(Type type)
        {
            if (type?.GetConstructor(Type.EmptyTypes) != null)
            {
                return Activator.CreateInstance(type);
            }

            return type != null ? FormatterServices.GetUninitializedObject(type) : null;
        }

        private static void ApplyValueToProperty(object value, SerializedProperty property)
        {
            property.managedReferenceValue = value;
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }
    }
}