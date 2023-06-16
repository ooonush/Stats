using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    public class RuntimeStatElement : VisualElement
    {
        public readonly FloatField BaseField;

        public readonly Label Label;
        
        private readonly IRuntimeStat _runtimeStat;

        public RuntimeStatElement(IRuntimeStat runtimeStat)
        {
            _runtimeStat = runtimeStat;

            style.flexDirection = FlexDirection.Row;

            Label = new Label
            {
                style =
                {
                    width = new StyleLength(161),
                    flexGrow = 0,
                    alignSelf = Align.Center
                }
            };

            BaseField = new FloatField(nameof(runtimeStat.Base));

            // style.maxHeight = new StyleLength(Length.Percent(100));

            BaseField.style.flexGrow = 1;
            BaseField.labelElement.style.minWidth = 0;

            Add(Label);
            Add(BaseField);

            BaseField.RegisterValueChangedCallback(OnBaseFieldChange);
            _runtimeStat.OnValueChanged += OnValueChange;

            UpdateRuntimeStatHeader();
            UpdateBaseField();
        }
        
        public void UpdateRuntimeStatHeader()
        {
            Label.text = StatsEditorHelper.GetRuntimeStatHeader(_runtimeStat);
        }

        private void OnValueChange(StatType statType, float _)
        {
            UpdateBaseField();
            UpdateRuntimeStatHeader();
        }

        private void OnBaseFieldChange(ChangeEvent<float> evt)
        {
            if (IsFloatValueNotEqual(evt))
            {
                _runtimeStat.Base = evt.newValue;
            }
            evt.StopPropagation();
        }
        
        private static bool IsFloatValueNotEqual(ChangeEvent<float> evt) =>
            Math.Abs(evt.newValue - evt.previousValue) > 0.0001;

        private void UpdateBaseField()
        {
            BaseField.value = _runtimeStat.Base;
        }

        public void Release()
        {
            _runtimeStat.OnValueChanged -= OnValueChange;
            BaseField.UnregisterValueChangedCallback(OnBaseFieldChange);
        }
    }
}
