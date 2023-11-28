using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AInspector
{
    public static class SerializedPropertyExtensions
    {
        private const BindingFlags BINDINGS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        public static void ApplyUnregisteredSerialization(SerializedObject serializedObject)
        {
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            serializedObject.Update();
            
            var component = serializedObject.targetObject as Component;
            if (component == null || !component.gameObject.scene.isLoaded) return;
            
            if (Application.isPlaying) return;
            EditorSceneManager.MarkSceneDirty(component.gameObject.scene);
        }

        public static T GetValue<T>(this SerializedProperty property)
        {
            object value = property.GetValue();
            if (value is T t) return t;
            return default;
        }

        public static object GetValue(this SerializedProperty property)
        {
            if (property == null) return default;
            ApplyUnregisteredSerialization(property.serializedObject);
            
            object obj = property.serializedObject.targetObject;
            
            string[] path = property.propertyPath.Split('.');
            for ( var i = 0; i < path.Length; i++)
            {
                if (path[i] == "Array")
                {
                    if (obj is IList a) obj = a[int.Parse(path[i+1][^2].ToString())];
                    continue;
                }
                
                Type type = obj.GetType();
                
                FieldInfo field = null;
                while (field == null)
                {
                    field = type.GetField(path[i], BINDINGS);

                    if (field != null) continue;
                    type = type.BaseType;
                    if (type == null)
                        break;
                }

                if (field != null) obj = field.GetValue(obj);
            }
            return obj;
        }

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