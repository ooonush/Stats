using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    public class RuntimeAttributesElement : VisualElement
    {
        private readonly List<RuntimeAttributeElement> _runtimeAttributeVisualElements = new();

        public RuntimeAttributesElement(ITraits target)
        {
            var runtimeAttributes = target.RuntimeAttributes;
            
            if (runtimeAttributes == null) return;
            
            foreach (IRuntimeAttribute runtimeAttribute in runtimeAttributes)
            {
                IRuntimeStat runtimeStat = target.RuntimeStats.Get(runtimeAttribute.AttributeType.MaxValueType);
                if (runtimeStat == null) continue;
                var runtimeAttributeVisualElement = new RuntimeAttributeElement(runtimeAttribute, runtimeStat);
                _runtimeAttributeVisualElements.Add(runtimeAttributeVisualElement);
                Add(runtimeAttributeVisualElement);
            }
        }
        
        public void ReleaseAllRuntimeAttribute()
        {
            foreach (RuntimeAttributeElement runtimeAttribute in _runtimeAttributeVisualElements)
            {
                runtimeAttribute.ReleaseCallback();
            }
        }
    }
}