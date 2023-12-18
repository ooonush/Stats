using System;

namespace Stats
{
    [Serializable]
    public sealed class TraitsClassItem : Getter<ITraitsClass>
    {
        public TraitsClassItem() : base(new TraitsClassAssetGetType())
        {
        }

        public TraitsClassItem(ITraitsClass traitsClass) : base(traitsClass)
        {
        }

        public TraitsClassItem(TraitsClassAsset traitsClass) : base(new TraitsClassAssetGetType(traitsClass))
        {
        }
    }
}