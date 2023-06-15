using System;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    public class RuntimeAttributeElement : VisualElement
    {
        private readonly IRuntimeAttribute _runtimeAttribute;
        private readonly IRuntimeStat _runtimeStat;
        
        private readonly Foldout _foldout;
        private readonly FloatField _valueField;
        private readonly RuntimeStatElement _runtimeStatElement;

        public RuntimeAttributeElement(IRuntimeAttribute runtimeAttribute, IRuntimeStat runtimeStat)
        {
            style.width = new StyleLength(Length.Percent(100));
            style.marginLeft = -3;
            
            _runtimeAttribute = runtimeAttribute;
            _runtimeStat = runtimeStat;

            _foldout = new Foldout
            {
                value = false,
                style =
                {
                    paddingBottom = 0
                }
            };

            _foldout.contentContainer.style.paddingLeft = 4;

            var minValueField = new FloatField(nameof(runtimeAttribute.MinValue))
            {
                value = _runtimeAttribute.MinValue
            };
            
            minValueField.labelElement.style.minWidth = new StyleLength(143);
            minValueField.SetEnabled(false);

            _valueField = new FloatField(nameof(runtimeStat.Value));

            _valueField.labelElement.style.minWidth = new StyleLength(143);

            _runtimeStatElement = new RuntimeStatElement(runtimeStat)
            {
                style =
                {
                    marginLeft = 3,
                    marginBottom = 5
                }
            };
            
            _runtimeStatElement.Label.style.minWidth = new StyleLength(143);

            _runtimeStatElement.Release();
            _runtimeStatElement.BaseField.RegisterValueChangedCallback(OnBaseFieldChanged);
            _valueField.RegisterValueChangedCallback(OnValueFieldChanged);
            runtimeAttribute.OnValueChanged += OnValueChange;
            runtimeStat.OnValueChanged += OnBaseChange;
            
            UpdateValueField();
            UpdateBaseField();
            UpdateRuntimeAttributeHeader();
            
            _foldout.Add(minValueField);
            _foldout.Add(_valueField);
            _foldout.Add(_runtimeStatElement);

            Add(_foldout);
        }
        
        private void OnValueFieldChanged(ChangeEvent<float> evt)
        {
            if (!EqualsFloatValue(evt))
            {
                if (evt.newValue > _runtimeAttribute.MaxValue)
                {
                    _runtimeAttribute.Value = _runtimeAttribute.MaxValue;
                    _valueField.value = _runtimeAttribute.MaxValue;
                    
                }
                else if (evt.newValue < _runtimeAttribute.MinValue)
                {
                    _runtimeAttribute.Value = _runtimeAttribute.MinValue;
                    _valueField.value = _runtimeAttribute.MinValue;
                }
                else
                {
                    _runtimeAttribute.Value = evt.newValue;
                }
            }
        }
 
        private void OnBaseFieldChanged(ChangeEvent<float> evt)
        {
            if (!EqualsFloatValue(evt))
            {
                if (evt.newValue < _runtimeAttribute.MinValue)
                {
                    _runtimeStat.Base = _runtimeAttribute.MinValue;
                    _valueField.value = _runtimeAttribute.MinValue;
                }
                else
                {
                    _runtimeStat.Base = evt.newValue;
                }

                _runtimeStatElement.BaseField.value = _runtimeStat.Base;
                _runtimeStatElement.UpdateRuntimeStatHeader();
            }
        }

        private static bool EqualsFloatValue(ChangeEvent<float> evt) =>
            Math.Abs(evt.newValue - evt.previousValue) <= 0.0001;

        private void OnValueChange(AttributeType attributeType, float value)
        {
            UpdateValueField();
            UpdateRuntimeAttributeHeader();
        }
        
        private void OnBaseChange(StatType statType, float value)
        {
            UpdateBaseField();
            UpdateRuntimeAttributeHeader();
        }

        private void UpdateValueField()
        {
            _valueField.value = _runtimeAttribute.Value;
        }
        
        private void UpdateBaseField()
        {
            _runtimeStatElement.BaseField.value = _runtimeAttribute.MaxValue;
        }
        
        private void UpdateRuntimeAttributeHeader()
        {
            _foldout.text = StatsEditorHelper.GetRuntimeAttributeHeader(_runtimeAttribute);
        }

        public void ReleaseCallback()
        {
            _runtimeAttribute.OnValueChanged -= OnValueChange;
            _runtimeStat.OnValueChanged -= OnBaseChange;
        }
    }
}