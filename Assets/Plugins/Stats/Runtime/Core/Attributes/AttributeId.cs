using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct AttributeId<TNumber> : IEquatable<AttributeId<TNumber>> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private string _id;

        public AttributeId(string id)
        {
            _id = id;
        }

        public static bool operator ==(AttributeId<TNumber> left, AttributeId<TNumber> right)
        {
            return left._id == right._id;
        }

        public static bool operator !=(AttributeId<TNumber> a, AttributeId<TNumber> b)
        {
            return !(a == b);
        }

        public static implicit operator string(AttributeId<TNumber> identifier)
        {
            return identifier._id;
        }

        public static implicit operator AttributeId<TNumber>(string id)
        {
            return new AttributeId<TNumber>(id);
        }

        public bool Equals(AttributeId<TNumber> other)
        {
            return _id == other._id;
        }

        public override bool Equals(object obj)
        {
            return obj is AttributeId<TNumber> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _id != null ? _id.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return _id;
        }
    }
}