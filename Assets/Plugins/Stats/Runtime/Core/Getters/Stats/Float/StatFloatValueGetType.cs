using System;
using AInspector;

namespace Stats
{
    [Serializable]
    [DropdownName("Stat (Float)")]
    internal class StatFloatValueGetType : StatValueGetType<TFloat>
    {
    }
}