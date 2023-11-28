using System;

namespace Stats
{
    [Serializable]
    internal class AttributeIdGetter : Getter<AttributeId>
    {
        public AttributeIdGetter() : base(new AttributeIdValueGetType())
        {
        }
    }

    [Serializable]
    internal class AttributeIdGetter<TNumber> : Getter<AttributeId<TNumber>> where TNumber : IStatNumber<TNumber>
    {
        public AttributeIdGetter() => SetDefaultEditorPropertyType<AttributeIdValueGetType<TNumber>>();
    }
}