using UnityEditor;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    [CustomPropertyDrawer(typeof(AttributeItem))]
    public class AttributeItemDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new AttributeItemVisualElement(property);
        }
    }
}