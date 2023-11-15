using System;

namespace Stats
{
    [Serializable]
    public sealed class TraitsClassGetter : Getter<ITraitsClass>
    {
        public TraitsClassGetter() => SetDefaultEditorPropertyType<TraitsClassAssetGetType>();
    }
}