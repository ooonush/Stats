using System;
using System.Collections.Generic;

namespace Stats
{
    [Serializable]
    public sealed class ConstantModifierList<TNumber> : ModifierList<ConstantModifier<TNumber>, TNumber>, IReadOnlyList<ConstantModifier<TDouble>>
        where TNumber : IStatNumber<TNumber>
    {
        IEnumerator<ConstantModifier<TDouble>> IEnumerable<ConstantModifier<TDouble>>.GetEnumerator()
        {
            foreach (ConstantModifier<TNumber> modifier in this)
            {
                yield return modifier.ToDoubleModifier();
            }
        }

        ConstantModifier<TDouble> IReadOnlyList<ConstantModifier<TDouble>>.this[int index] => base[index].ToDoubleModifier();
    }
}