using System;
using AInspector;

namespace Stats
{
    [Serializable]
    [DropdownName("Value From Script")]
    public sealed class ObjectValueGetType : IGetType<object>
    {
        public object Value;

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