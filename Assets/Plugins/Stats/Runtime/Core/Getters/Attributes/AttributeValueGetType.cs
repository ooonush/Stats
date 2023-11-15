using System;
using AInspector;
using UnityEngine;

namespace Stats
{
    [Serializable]
    [DropdownName("Attribute")]
    internal abstract class AttributeValueGetType<TNumber> : AttributeGetType<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private Attribute<TNumber> _attribute;
        public sealed override IAttribute<TNumber> Get() => _attribute;
    }
}