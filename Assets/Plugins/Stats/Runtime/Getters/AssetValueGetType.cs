using System;
using AInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Stats
{
    [Serializable]
    [DropdownName("Value From Script")]
    public sealed class AssetValueGetType : IGetType<Object>
    {
        [SerializeField] public Object Value;

        public AssetValueGetType(Object value)
        {
            Value = value;
        }

        public AssetValueGetType()
        {
        }

        public Object Get() => Value;
    }
}