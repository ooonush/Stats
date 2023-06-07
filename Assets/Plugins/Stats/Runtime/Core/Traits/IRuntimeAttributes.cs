using System.Collections.Generic;

namespace Stats
{
    public interface IRuntimeAttributes<out TRuntimeAttribute> : IReadOnlyCollection<TRuntimeAttribute>
        where TRuntimeAttribute : IRuntimeAttribute
    {
        event AttributeValueChangedAction OnValueChanged;
        TRuntimeAttribute Get(AttributeType attributeType);
        bool Contains(AttributeType attributeType);
    }
}