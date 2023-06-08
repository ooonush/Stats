using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    public class RuntimeAttributesVisualElement : VisualElement
    {
        private readonly Dictionary<AttributeType, Label> _labels = new();
        private readonly IRuntimeAttributes<IRuntimeAttribute> _runtimeAttributes;
        
        public RuntimeAttributesVisualElement(ITraits target)
        {
            _runtimeAttributes = target.RuntimeAttributes;
            
            if (_runtimeAttributes == null) return;
            
            foreach (IRuntimeAttribute runtimeAttribute in _runtimeAttributes)
            {
                var label = new Label();
                _labels.Add(runtimeAttribute.AttributeType, label);
                runtimeAttribute.OnValueChanged += OnRuntimeAttributeChange;
                Add(label);
                UpdateRuntimeAttributeHeader(runtimeAttribute.AttributeType);
            }
        }
        
        public void ReleaseAllCallback()
        {
            foreach (IRuntimeAttribute runtimeAttribute in _runtimeAttributes)
            {
                runtimeAttribute.OnValueChanged -= OnRuntimeAttributeChange;
            }
        }
        
        private void OnRuntimeAttributeChange(AttributeType attributeType, float _)
        {
            UpdateRuntimeAttributeHeader(attributeType);
        }
        
        
        private void UpdateRuntimeAttributeHeader(AttributeType type)
        {
            _labels[type].text = StatsEditorHelper.GetRuntimeAttributeHeader(_runtimeAttributes.Get(type));
        }
    }
}