using System;
using AInspector;

namespace Stats
{
    [Serializable]
    [DropdownName("Stat (Int)")]
    internal sealed class StatIntValueGetType : StatValueGetType<TInt>
    {
    }
}