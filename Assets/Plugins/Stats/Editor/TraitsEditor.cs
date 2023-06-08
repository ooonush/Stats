using UnityEditor;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    [CustomEditor(typeof(Traits))]
    [CanEditMultipleObjects]
    public class TraitsEditor : UnityEditor.Editor
    {
        private TraitsVisualElement _traitsVisualElement; 
            
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            _traitsVisualElement = new TraitsVisualElement(target as Traits, serializedObject);
            root.Add(_traitsVisualElement);
            return root;
        }

        private void OnDestroy() => _traitsVisualElement?.ReleaseAllCallback();
    }
}