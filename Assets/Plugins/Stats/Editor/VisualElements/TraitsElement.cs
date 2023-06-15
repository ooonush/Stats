using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    public class TraitsElement : VisualElement
    {
        private readonly ITraits _traits;
        private readonly PropertyField _fieldTraitsClass;
        private readonly SerializedProperty _traitsClassProperty;

        private readonly VisualElement _statLabelContainer;
        private readonly VisualElement _statContainer;
        private readonly VisualElement _attributeLabelContainer;
        private readonly VisualElement _attributeContainer;

        private readonly Label _statLabel;
        private readonly Label _attributeLabel;

        private TraitsClassBase _oldTraitsClass;
        private RuntimeStatsElement _runtimeStatsElement;
        private RuntimeAttributesElement _runtimeAttributeElement;

        public TraitsElement(ITraits traits, SerializedObject serializedObject)
        {
            _traits = traits;

            _traitsClassProperty = serializedObject.FindProperty("_traitsClass");

            _oldTraitsClass = StatsEditorHelper.GetValue<TraitsClassBase>(_traitsClassProperty);
            
            _fieldTraitsClass = new PropertyField(_traitsClassProperty);
            _statLabelContainer = new VisualElement();
            _statLabel = new Label { text = "<b>Stats</b>" };
            _statContainer = new VisualElement();
            _attributeLabelContainer = new VisualElement();
            _attributeLabel = new Label { text = "<b>Attributes</b>" };
            _attributeContainer = new VisualElement();

            _statContainer.style.marginLeft = 3f;
            _statContainer.style.marginTop = 5f; 
            _statLabel.style.marginTop = 5f;
            _statLabel.style.marginLeft = 3f;
            _statLabel.style.marginBottom = -2f;
            _attributeContainer.style.marginLeft = 3f;
            _attributeContainer.style.marginTop = 5f; 
            _attributeLabel.style.marginTop = 15f;
            _attributeLabel.style.marginLeft = 3f;
            
            _statLabel.style.fontSize = 12;
            _attributeLabel.style.fontSize = 12;
            
            _fieldTraitsClass.RegisterValueChangeCallback(OnTraitsClassChange);

            Add(_fieldTraitsClass);
            Add(_statLabelContainer);
            Add(_statContainer);
            Add(_attributeLabelContainer);
            Add(_attributeContainer);

            RepaintElements();
        }

        public void ReleaseAllCallback()
        {
            _runtimeStatsElement?.ReleaseAllCallback();
            _runtimeAttributeElement?.ReleaseAllRuntimeAttribute();
        }
        
        private void OnTraitsClassChange(SerializedPropertyChangeEvent evt)
        {
            var newTraitsClass = StatsEditorHelper.GetValue<TraitsClassBase>(_traitsClassProperty);
            if (_oldTraitsClass == newTraitsClass) return;
            RepaintElements();
            _oldTraitsClass = newTraitsClass;
        }

        private void RepaintElements()
        {
            _statLabelContainer.Clear();
            _statContainer.Clear();
            _attributeLabelContainer.Clear();
            _attributeContainer.Clear();
            
            _statLabelContainer.Add(_statLabel);
            _attributeLabelContainer.Add(_attributeLabel);
            
            if (Application.isPlaying)
            {
                _fieldTraitsClass.SetEnabled(false);
                
                _runtimeStatsElement = new RuntimeStatsElement(_traits);
                _statContainer.Add(_runtimeStatsElement);

                _runtimeAttributeElement = new RuntimeAttributesElement(_traits);
                _attributeContainer.Add(_runtimeAttributeElement);
                
                if (_runtimeStatsElement.childCount == 0) _statLabel.RemoveFromHierarchy();
                if (_runtimeAttributeElement.childCount == 0) _attributeLabel.RemoveFromHierarchy();
            }
            
            if (_statContainer.childCount == 0) _statLabel.RemoveFromHierarchy();
            if (_attributeContainer.childCount == 0) _attributeLabel.RemoveFromHierarchy();
        }
    }
}