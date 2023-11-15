#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Stats
{
    /// <summary>
    /// ScriptableObject that stores a Id for unique identification. The population of this field is implemented
    /// inside an Editor script.
    /// </summary>
    public abstract class IdScriptableObject : ScriptableObject
    {
        [HideInInspector]
        [SerializeField] private string _id;

        public string Id => _id;

        protected void OnValidate()
        {
#if UNITY_EDITOR
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out string guid, out long _);
            if (guid != null && guid != _id)
            {
                _id = guid;
            }
#endif
            OnValidation();
        }

        protected virtual void OnValidation()
        {
            
        }
    }
}