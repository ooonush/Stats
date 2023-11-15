using AInspector;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    [CustomPropertyDrawer(typeof(GetType<>), true)]
    public class PropertyTypeGetDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var children = property.GetChildren();
            return children.Count == 1 ? new PropertyField(children[0], preferredLabel) : new PropertyField(property, preferredLabel);
        }
    }
}