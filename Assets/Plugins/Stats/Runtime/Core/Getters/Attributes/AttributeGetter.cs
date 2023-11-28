using System;

namespace Stats
{
    [Serializable]
    internal sealed class AttributeGetter : Getter<IAttribute>
    {
        public AttributeGetter() => SetDefaultEditorPropertyType<AttributeValueGetType<TFloat>>();
    }

    [Serializable]
    internal sealed class AttributeGetter<TNumber> : Getter<IAttribute<TNumber>> where TNumber : IStatNumber<TNumber>
    {
        public AttributeGetter() => SetDefaultEditorPropertyType<AttributeValueGetType<TNumber>>();
    }
}