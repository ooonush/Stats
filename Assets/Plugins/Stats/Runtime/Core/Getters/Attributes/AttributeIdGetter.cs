using System;

namespace Stats
{
    [Serializable]
    internal class AttributeIdGetter<TNumber> : Getter<AttributeId<TNumber>> where TNumber : IStatNumber<TNumber>
    {
        public AttributeIdGetter() => SetDefaultEditorPropertyType<AttributeIdValueGetType<TNumber>>();
    }
}