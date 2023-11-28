using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    [CustomPropertyDrawer(typeof(StatId), true)]
    [CustomPropertyDrawer(typeof(StatId<>), true)]
    [CustomPropertyDrawer(typeof(AttributeId), true)]
    [CustomPropertyDrawer(typeof(AttributeId<>), true)]
    public class IdDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new PropertyField(property.FindPropertyRelative("_id"), preferredLabel);
        }
    }
}