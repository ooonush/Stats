using AInspector;
using Stats;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Plugins.Stats.Numerics
{
    [CustomPropertyDrawer(typeof(IStatNumber<>), true)]
    public class TNumberDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var children = property.GetChildren();
            
            return children.Count == 1 ? new PropertyField(children[0], property.displayName) : new PropertyField(property);
        }
    }
}