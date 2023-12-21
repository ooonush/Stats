using System;
using AInspector;

namespace Stats
{
    [Serializable]
    [DropdownName("Asset (Int)")]
    internal sealed class StatIntIdFromAssetGetType : StatIdAssetGetType<TInt>
    {
    }
}