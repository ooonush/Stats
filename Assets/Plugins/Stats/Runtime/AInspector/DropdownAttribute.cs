using UnityEngine;

namespace AInspector
{
    public sealed class DropdownAttribute : PropertyAttribute
    {
        public readonly bool IncludeNull;

        public DropdownAttribute(bool includeNull = true)
        {
            IncludeNull = includeNull;
        }
    }
}