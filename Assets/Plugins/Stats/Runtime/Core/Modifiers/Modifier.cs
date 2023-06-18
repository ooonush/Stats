using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct Modifier
    {
        [SerializeField] private ModifierType _type;
        [SerializeField] private float _value;

        public ModifierType Type => _type;
        public float Value => _value;

        public Modifier(ModifierType type, float value)
        {
            _type = type;
            _value = value;
        }
    }
}