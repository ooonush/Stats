using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct AttributeIdItem
    {
        [SerializeField] private AttributeIdGetter _attributeId;
        public AttributeId AttributeId => _attributeId.Get();
    }

    [Serializable]
    public struct AttributeIdItem<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private AttributeIdGetter<TNumber> _attributeId;
        public AttributeId<TNumber> AttributeId => _attributeId.Get();
    }
}