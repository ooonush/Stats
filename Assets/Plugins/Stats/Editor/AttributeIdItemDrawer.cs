using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    [CustomPropertyDrawer(typeof(AttributeIdItem), true)]
    [CustomPropertyDrawer(typeof(AttributeIdItem<>), true)]
    public class AttributeIdItemDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new PropertyField(property.FindPropertyRelative("_attributeId"), preferredLabel);
        }
    }
}