using UnityEditor;
using UnityEngine.UIElements;

namespace Stats.Editor
{
    [CustomPropertyDrawer(typeof(StatItem))]
    public class StatItemDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new StatItemVisualElement(property);
        }
    }
}