using System;
using UnityEditor;
using UnityEngine;

namespace Stats
{
    /// <summary>
    /// ScriptableObject that stores a Id for unique identification. The population of this field is implemented
    /// inside an Editor script.
    /// </summary>
    [Serializable]
    public abstract class IdScriptableObject : ScriptableObject
    {
        [HideInInspector]
        [SerializeField] private string _id;

        public string Id => _id;

        private void OnValidate()
        {
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out string guid, out long _);
            if (_id != null && guid != _id)
            {
                _id = guid;
            }
        }
    }
}