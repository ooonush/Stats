using System;
using AInspector;
using UnityEngine;

namespace Stats
{
    [Serializable]
    [DropdownName("String")]
    internal abstract class AttributeIdValueGetType<TNumber> : AttributeIdGetType<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private AttributeId<TNumber> _attributeId;
        public sealed override AttributeId<TNumber> Get() => _attributeId;
    }
}