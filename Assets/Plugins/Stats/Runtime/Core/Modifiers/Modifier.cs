using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct Modifier
    {
        [SerializeField] private float _value;

        public float Value => _value;

        public Modifier(float value)
        {
            _value = value;
        }
    }
}