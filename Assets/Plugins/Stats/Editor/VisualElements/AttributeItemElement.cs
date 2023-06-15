using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    public class AttributeItemElement : VisualElement
    {
        private readonly SerializedProperty _property;

        private readonly SerializedProperty _attr;
        private readonly SerializedProperty _changeStartPercent;
        private readonly SerializedProperty _startPercent;

        private readonly VisualElement _contentChangeStartPercent = new();
        private readonly VisualElement _contentStartPercent = new();

        private readonly PropertyField _fieldChangeStartPercent;
        private readonly PropertyField _fieldStartPercent;

        private AttributeType _oldAttributeType;
        private bool _oldChangePercent;
        private float _oldPercent;

        private readonly AttributeItem _attributeItem;

        private readonly Foldout _foldout = new();

        public AttributeItemElement(SerializedProperty property)
        {
            _property = property;

            _attr = property.FindPropertyRelative("_attributeType");
            _changeStartPercent = property.FindPropertyRelative("_changeStartPercent");
            _startPercent = property.FindPropertyRelative("_startPercent");

            var fieldAttr = new PropertyField(_attr);
            _fieldChangeStartPercent = new PropertyField(_changeStartPercent);
            _fieldStartPercent = new PropertyField(_startPercent, "");

            _attributeItem = StatsEditorHelper.GetValue<AttributeItem>(property);

            UpdateHeaderText();
            UpdateValues();

            fieldAttr.RegisterValueChangeCallback(OnAttrChange);
            _fieldChangeStartPercent.RegisterValueChangeCallback(OnChangeStartPercentChange);
            _fieldStartPercent.RegisterValueChangeCallback(OnStartPercentChange);

            _foldout.Add(fieldAttr);
            _foldout.Add(_contentChangeStartPercent);
            _foldout.Add(_contentStartPercent);

            Add(_foldout);

            if (_oldAttributeType == null) return;

            _contentChangeStartPercent.Add(_fieldChangeStartPercent);
            if (_changeStartPercent.boolValue) _contentStartPercent.Add(_fieldStartPercent);
        }

        private void OnAttrChange(SerializedPropertyChangeEvent changeEvent)
        {
            var newAttributeType = StatsEditorHelper.GetValue<AttributeType>(_attr);
            bool isNotEmpty = newAttributeType != null;
            if (_oldAttributeType == newAttributeType)
            {
                UpdateHeaderText();
                return;
            }

            _oldAttributeType = newAttributeType;
            UpdateValues();

            RepaintChangePercent(isNotEmpty);
            RepaintPercent(isNotEmpty && _changeStartPercent.boolValue);

            if (isNotEmpty)
            {
                _startPercent.floatValue = newAttributeType.StartPercent;
            }

            UpdateHeaderText();
        }

        private void UpdateValues()
        {
            _oldAttributeType = StatsEditorHelper.GetValue<AttributeType>(_attr);
            _oldChangePercent = StatsEditorHelper.GetValue<bool>(_changeStartPercent);
            _oldPercent = _attributeItem.StartPercent;
        }

        private void OnChangeStartPercentChange(SerializedPropertyChangeEvent changeEvent)
        {
            bool isOn = changeEvent.changedProperty.boolValue;
            if (_oldChangePercent == isOn) return;

            UpdateValues();
            ResetPercent();
            RepaintPercent(isOn);
            UpdateHeaderText();
        }

        private void OnStartPercentChange(SerializedPropertyChangeEvent changeEvent)
        {
            if (Math.Abs(_oldPercent - changeEvent.changedProperty.floatValue) < 0.0001) return;


            UpdateValues();
            UpdateHeaderText();
        }

        private void ResetPercent()
        {
            _startPercent.floatValue = _oldAttributeType.StartPercent;
        }

        private void RepaintChangePercent(bool isShow)
        {
            _contentChangeStartPercent.Clear();

            if (!isShow) return;

            _contentChangeStartPercent.Add(_fieldChangeStartPercent);
            _fieldChangeStartPercent.Bind(_attr.serializedObject);
        }

        private void RepaintPercent(bool isShow)
        {
            _contentStartPercent.Clear();

            if (!isShow) return;

            _contentStartPercent.Add(_fieldStartPercent);
            _fieldStartPercent.Bind(_changeStartPercent.serializedObject);
        }

        private void UpdateHeaderText()
        {
            AttributeType attributeValue = _oldAttributeType;
            _foldout.text = !attributeValue ? "<color=yellow>Select Attribute</color>"
                : StatsEditorHelper.GetAttributeItemHeader(_attributeItem,
                    StatsEditorHelper.GetLabel<AttributeItem>(_property));
        }
    }
}