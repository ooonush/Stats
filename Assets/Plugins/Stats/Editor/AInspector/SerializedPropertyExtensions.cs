using System.Collections.Generic;
using UnityEditor;

namespace AInspector
{
    public static class SerializedPropertyExtensions
    {
        public static IReadOnlyList<SerializedProperty> GetChildren(this SerializedProperty property)
        {
            SerializedProperty iteratorProperty = property.Copy();
            SerializedProperty endProperty = iteratorProperty.GetEndProperty();

            var properties = new List<SerializedProperty>();

            if (!iteratorProperty.NextVisible(true)) return properties;
            do
            {
                if (SerializedProperty.EqualContents(iteratorProperty, endProperty)) break;
                properties.Add(iteratorProperty.Copy());
            } while (iteratorProperty.NextVisible(false));

            return properties;
        }
    }
}