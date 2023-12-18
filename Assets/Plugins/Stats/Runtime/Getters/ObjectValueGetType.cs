using System;
using AInspector;
using UnityEngine;

namespace Stats
{
    [Serializable]
    [DropdownName("Value From Script")]
    public sealed class ObjectValueGetType : IGetType<object>
    {
        [SerializeReference] public object Value;

        public ObjectValueGetType(object value)
        {
            Value = value;
        }

        public ObjectValueGetType()
        {
        }

        public object Get() => Value;
    }
}