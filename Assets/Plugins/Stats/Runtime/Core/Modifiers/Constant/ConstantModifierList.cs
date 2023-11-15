using System;

namespace Stats
{
    [Serializable]
    public sealed class ConstantModifierList<TNumber> : ModifierList<ConstantModifier<TNumber>, TNumber>
        where TNumber : IStatNumber<TNumber>
    {
    }
}