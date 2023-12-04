using System;

namespace Stats
{
    [Serializable]
    public sealed class AttributeItem : Getter<IAttribute>
    {
        public AttributeItem() => SetDefaultEditorPropertyType<AttributeValueGetType<TFloat>>();
    }

    [Serializable]
    public sealed class AttributeItem<TNumber> : Getter<IAttribute<TNumber>> where TNumber : IStatNumber<TNumber>
    {
        public AttributeItem() => SetDefaultEditorPropertyType<AttributeValueGetType<TNumber>>();
    }
}