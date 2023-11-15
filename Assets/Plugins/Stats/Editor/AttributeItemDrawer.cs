using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    [CustomPropertyDrawer(typeof(AttributeItem<>), true)]
    public class AttributeItemDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new PropertyField(property.FindPropertyRelative("_attribute"), preferredLabel);
        }
    }
}