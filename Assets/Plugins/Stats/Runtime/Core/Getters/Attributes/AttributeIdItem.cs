using System;

namespace Stats
{
    [Serializable]
    public class AttributeIdItem : Getter<AttributeId>
    {
        public AttributeIdItem() : base(new AttributeIdValueGetType())
        {
        }

        public AttributeIdItem(AttributeId attributeId) : base(new AttributeIdValueGetType(attributeId))
        {
        }
    }

    [Serializable]
    public class AttributeIdItem<TNumber> : Getter<AttributeId<TNumber>> where TNumber : IStatNumber<TNumber>
    {
        public AttributeIdItem() => SetDefaultEditorPropertyType<AttributeIdValueGetType<TNumber>>();

        public AttributeIdItem(AttributeId<TNumber> attributeId)
        {
            Property = new AttributeIdValueGetType(attributeId);
        }

        public AttributeIdItem(string attributeId)
        {
            Property = new AttributeIdValueGetType(attributeId);
        }

        public override AttributeId<TNumber> Value
        {
            get
            {
                if (Property is AttributeIdValueGetType stringGetType)
                {
                    return (AttributeId<TNumber>)stringGetType.Get();
                }
                return base.Value;
            }
        }
    }
}