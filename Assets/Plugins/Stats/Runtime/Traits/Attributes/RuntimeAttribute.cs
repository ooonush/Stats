namespace Stats
{
    public sealed class RuntimeAttribute<TNumber> : RuntimeAttributeBase<TNumber> where TNumber : IStatNumber<TNumber>
    {
        internal RuntimeAttribute(ITraits traits, IAttribute<TNumber> attribute) : base(traits, attribute)
        {
        }
    }
}