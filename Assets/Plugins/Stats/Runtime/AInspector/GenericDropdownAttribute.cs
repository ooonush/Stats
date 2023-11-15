using UnityEngine;

namespace AInspector
{
    public sealed class GenericDropdownAttribute : PropertyAttribute
    {
        public readonly string GenericGetterName;

        public GenericDropdownAttribute(string genericGetterName)
        {
            GenericGetterName = genericGetterName;
        }
    }
}