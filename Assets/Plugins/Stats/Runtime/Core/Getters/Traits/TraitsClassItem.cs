using System;

namespace Stats
{
    [Serializable]
    public sealed class TraitsClassItem : Getter<ITraitsClass>
    {
        public TraitsClassItem() => SetDefaultEditorPropertyType<TraitsClassAssetGetType>();
    }
}