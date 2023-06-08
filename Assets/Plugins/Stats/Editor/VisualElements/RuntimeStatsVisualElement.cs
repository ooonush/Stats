using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    public class RuntimeStatsVisualElement : VisualElement
    {
        private readonly Dictionary<StatType, Label> _labels = new();
        private readonly IRuntimeStats<IRuntimeStat> _runtimeStats;
        private readonly IRuntimeAttributes<IRuntimeAttribute> _runtimeAttributes;
        
        public RuntimeStatsVisualElement(ITraits target)
        {
            _runtimeStats = target.RuntimeStats;
            _runtimeAttributes = target.RuntimeAttributes;

            if (_runtimeStats == null) return;
            
            foreach (IRuntimeStat runtimeStat in _runtimeStats)
            {
                if (IsStatContainsInRuntimeAttributes(runtimeStat.StatType)) continue;
                        
                var label = new Label();
                _labels.Add(runtimeStat.StatType, label);
                runtimeStat.OnValueChanged += OnRuntimeStatChange;
                Add(label);
                UpdateRuntimeStatHeader(runtimeStat.StatType);
            }
        }

        public void ReleaseAllCallback()
        {
            foreach (IRuntimeStat runtimeStat in _runtimeStats)
            {
                runtimeStat.OnValueChanged -= OnRuntimeStatChange;
            }
        }

        private void OnRuntimeStatChange(StatType statType, float _)
        {
            UpdateRuntimeStatHeader(statType);
        }
        
        private void UpdateRuntimeStatHeader(StatType type)
        {
            _labels[type].text = StatsEditorHelper.GetRuntimeStatHeader(_runtimeStats.Get(type));
        }
        
        private bool IsStatContainsInRuntimeAttributes(StatType statType)
        {
            return _runtimeAttributes.Any(attribute => attribute.AttributeType.MaxValueType == statType);
        }
    }
}