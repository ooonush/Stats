using System;
using AInspector;

namespace Stats
{
    [Serializable]
    [DropdownName("Attribute (Int)")]
    internal sealed class AttributeIntValueGetType : AttributeValueGetType<TInt>
    {
    }
}