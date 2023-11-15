using System;
using UnityEngine;

namespace AInspector
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
    public sealed class DropdownNameAttribute : PropertyAttribute
    {
        public readonly string Name;

        public DropdownNameAttribute(string name)
        {
            Name = name;
        }
    }
}