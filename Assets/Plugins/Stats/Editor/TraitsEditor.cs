using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    [CustomEditor(typeof(Traits))]
    [CanEditMultipleObjects]
    public class TraitsEditor : UnityEditor.Editor
    {
        private TraitsElement _traitsElement;

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            _traitsElement = new TraitsElement(target as Traits, serializedObject);
            root.Add(_traitsElement);
            return root;
        }

        private void OnDestroy() => _traitsElement?.ReleaseAllCallback();
    }
}