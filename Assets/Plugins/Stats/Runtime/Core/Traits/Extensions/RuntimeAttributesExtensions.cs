namespace Stats
{
    public static class RuntimeAttributesExtensions
    {
        public static IRuntimeAttribute Get(this IRuntimeAttributes attributes, AttributeIdItem attributeIdItem)
        {
            return attributes.Get(attributeIdItem.Value);
        }

        public static IRuntimeAttribute<TNumber> Get<TNumber>(this IRuntimeAttributes attributes, AttributeIdItem attributeIdItem) where TNumber : IStatNumber<TNumber>
        {
            return attributes.Get((AttributeId<TNumber>)attributeIdItem.Value);
        }

        public static IRuntimeAttribute<TNumber> Get<TNumber>(this IRuntimeAttributes attributes, AttributeIdItem<TNumber> attributeIdItem) where TNumber : IStatNumber<TNumber>
        {
            return attributes.Get(attributeIdItem.Value);
        }

        public static RuntimeAttribute<TNumber> Get<TNumber>(this RuntimeAttributes attributes, AttributeIdItem attributeIdItem) where TNumber : IStatNumber<TNumber>
        {
            return attributes.Get((AttributeId<TNumber>)attributeIdItem.Value);
        }

        public static RuntimeAttribute<TNumber> Get<TNumber>(this RuntimeAttributes attributes, AttributeIdItem<TNumber> attributeIdItem) where TNumber : IStatNumber<TNumber>
        {
            return attributes.Get(attributeIdItem.Value);
        }
    }
}