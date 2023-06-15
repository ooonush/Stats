using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    public class StatItemElement : VisualElement
    {
        private readonly SerializedProperty _property;

        private readonly SerializedProperty _stat;
        private readonly SerializedProperty _changeValue;
        private readonly SerializedProperty _value;
        private readonly SerializedProperty _changeFormula;

        private readonly PropertyField _fieldChangeValue;
        private readonly PropertyField _fieldValue;
        private readonly PropertyField _fieldChangeFormula;
        private readonly PropertyField _fieldFormula;

        private readonly Foldout _foldout = new();
        private readonly VisualElement _contentChangeValue = new ();
        private readonly VisualElement _contentValue = new ();
        private readonly VisualElement _contentChangeFormula = new ();
        private readonly VisualElement _contentFormula = new ();

        private readonly StatItem _statItem;

        private Stat _oldStat;
        private bool _oldChangeValue;
        private float _oldValue;
        private bool _oldChangeFormula;

        public StatItemElement(SerializedProperty property)
        {
            _property = property;
            
            _stat = property.FindPropertyRelative("_stat");
            _changeValue = property.FindPropertyRelative("_changeBase");
            _value = property.FindPropertyRelative("_base");
            _changeFormula = property.FindPropertyRelative("_changeFormula");
            SerializedProperty formula = property.FindPropertyRelative("_formula");

            var fieldStat = new PropertyField(_stat);
            _fieldChangeValue = new PropertyField(_changeValue);
            _fieldValue = new PropertyField(_value, "");
            _fieldChangeFormula = new PropertyField(_changeFormula);
            _fieldFormula = new PropertyField(formula, "");

            _statItem = StatsEditorHelper.GetValue<StatItem>(property);

            UpdateValues();
            UpdateHeaderText();

            fieldStat.RegisterValueChangeCallback(OnStatChange);
            _fieldChangeValue.RegisterValueChangeCallback(OnChangeValueChange);
            _fieldValue.RegisterValueChangeCallback(OnValueChange);
            _fieldChangeFormula.RegisterValueChangeCallback(OnChangeFormulaChange);

            _foldout.Add(fieldStat);
            _foldout.Add(_contentChangeValue);
            _foldout.Add(_contentValue);
            _foldout.Add(_contentChangeFormula);
            _foldout.Add(_contentFormula);

            Add(_foldout);

            if (_oldStat == null) return;

            _contentChangeValue.Add(_fieldChangeValue);
            if (_changeValue.boolValue) _contentValue.Add(_fieldValue);
            _contentChangeFormula.Add(_fieldChangeFormula);
            if (_changeFormula.boolValue) _contentFormula.Add(_fieldFormula);
        }

        private void OnStatChange(SerializedPropertyChangeEvent changeEvent)
        {
            var newStat = StatsEditorHelper.GetValue<Stat>(_stat);
            bool isNotEmpty = newStat != null;
            if (newStat == _oldStat)
            {
                UpdateHeaderText();
                return;
            }

            _oldStat = newStat;
            UpdateValues();

            RepaintChangeValue(isNotEmpty, changeEvent);
            RepaintChangeFormula(isNotEmpty, changeEvent);
            RepaintValue(isNotEmpty && _changeValue.boolValue, changeEvent);
            RepaintFormula(isNotEmpty && _changeFormula.boolValue, changeEvent);

            if (isNotEmpty)
            {
                _value.floatValue = newStat.Base;
            }

            UpdateHeaderText();
        }

        private void UpdateValues()
        {
            _oldStat = StatsEditorHelper.GetValue<Stat>(_stat);
            _oldChangeValue = StatsEditorHelper.GetValue<bool>(_changeValue);
            _oldValue = _statItem.Base;
            _oldChangeFormula = StatsEditorHelper.GetValue<bool>(_changeFormula);
        }

        private void OnChangeValueChange(SerializedPropertyChangeEvent changeEvent)
        {
            bool isOn = changeEvent.changedProperty.boolValue;

            if (isOn == _oldChangeValue) return;

            UpdateValues();
            ResetValue();
            RepaintValue(isOn, changeEvent);
            UpdateHeaderText();
        }

        private void OnValueChange(SerializedPropertyChangeEvent changeEvent)
        {
            if (Math.Abs(_oldValue - changeEvent.changedProperty.floatValue) < 0.0001) return;
            UpdateValues();
            UpdateHeaderText();
        }

        private void OnChangeFormulaChange(SerializedPropertyChangeEvent changeEvent)
        {
            bool isOn = changeEvent.changedProperty.boolValue;

            if (isOn == _oldChangeFormula) return;

            RepaintFormula(isOn, changeEvent);

            UpdateValues();
        }

        private void RepaintChangeValue(bool isShow, SerializedPropertyChangeEvent changeEvent)
        {
            _contentChangeValue.Clear();
            _fieldChangeValue.Unbind();
            
            if (!isShow) return;
            
            _contentChangeValue.Add(_fieldChangeValue);
            _fieldChangeValue.Bind(changeEvent.changedProperty.serializedObject);
        }

        private void RepaintValue(bool isShown, SerializedPropertyChangeEvent changeEvent)
        {
            _contentValue.Clear();
            _fieldValue.Unbind();
            if (!isShown) return;
            _contentValue.Add(_fieldValue);
            _fieldValue.Bind(changeEvent.changedProperty.serializedObject);
        }

        private void RepaintChangeFormula(bool isShow, SerializedPropertyChangeEvent changeEvent)
        {
            _contentChangeFormula.Clear();
            _fieldChangeFormula.Unbind();
            
            if (!isShow) return;
            
            _contentChangeFormula.Add(_fieldChangeFormula);
            _fieldChangeFormula.Bind(changeEvent.changedProperty.serializedObject);
        }

        private void RepaintFormula(bool isShow, SerializedPropertyChangeEvent changeEvent)
        {
            _contentFormula.Clear();
            _fieldFormula.Unbind();
            
            if (!isShow) return;
            
            _contentFormula.Add(_fieldFormula);
            _fieldFormula.Bind(changeEvent.changedProperty.serializedObject);
        }

        private void ResetValue()
        {
            _value.floatValue = _oldStat.Base;
        }

        private void UpdateHeaderText()
        {
            _foldout.text = _stat.objectReferenceValue == null ? 
                "<color=yellow>Select Stat</color>" : 
                StatsEditorHelper.GetStatItemHeader(_statItem, StatsEditorHelper.GetLabel<StatItem>(_property));
        }
    }
}