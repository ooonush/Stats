using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    public class RuntimeStatsElement : VisualElement
    {
        private readonly List<RuntimeStatElement> _runtimeStatVisualElements = new();
        private readonly IRuntimeAttributes<IRuntimeAttribute> _runtimeAttributes;
        
        public RuntimeStatsElement(ITraits target)
        {
            var runtimeStats = target.RuntimeStats;
            _runtimeAttributes = target.RuntimeAttributes;

            if (runtimeStats == null) return;
            
            foreach (IRuntimeStat runtimeStat in runtimeStats)
            {
                if (IsStatContainsInRuntimeAttributes(runtimeStat.StatType)) continue;

                var runtimeStatVisualElement = new RuntimeStatElement(runtimeStat);
                _runtimeStatVisualElements.Add(runtimeStatVisualElement);
                Add(runtimeStatVisualElement);
            }
        }

        private bool IsStatContainsInRuntimeAttributes(StatType statType)
        {
            return _runtimeAttributes.Any(attribute => attribute.AttributeType.MaxValueType == statType);
        }
        
        public void ReleaseAllCallback()
        {
            foreach (RuntimeStatElement runtimeStat in _runtimeStatVisualElements)
            {
                runtimeStat.Release();
            }
        }
    }
}