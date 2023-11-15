using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct AttributeIdItem<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private AttributeIdGetter<TNumber> _attributeId;
        public AttributeId<TNumber> AttributeId => _attributeId.Get();
    }
}