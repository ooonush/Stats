using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    [CustomPropertyDrawer(typeof(StatItem), true)]
    [CustomPropertyDrawer(typeof(StatItem<>), true)]
    public class StatItemDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new PropertyField(property.FindPropertyRelative("_stat"), preferredLabel);
        }
    }
}