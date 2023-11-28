using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct AttributeItem
    {
        [SerializeField] private AttributeGetter _attribute;
        public IAttribute Attribute => _attribute.Get();
    }

    [Serializable]
    public struct AttributeItem<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private AttributeGetter<TNumber> _attribute;
        public IAttribute<TNumber> Attribute => _attribute.Get();
    }
}