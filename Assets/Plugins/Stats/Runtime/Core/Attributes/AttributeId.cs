using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct AttributeId
    {
        [SerializeField] private string _id;
        
        public AttributeId(string id)
        {
            _id = id;
        }

        public static bool operator ==(AttributeId left, AttributeId right)
        {
            return left._id == right._id;
        }

        public static bool operator !=(AttributeId a, AttributeId b)
        {
            return !(a == b);
        }

        public static implicit operator string(AttributeId identifier)
        {
            return identifier._id;
        } 

        public static implicit operator AttributeId(string id)
        {
            return new AttributeId(id);
        }

        public bool Equals(AttributeId other)
        {
            return _id == other._id;
        }

        public override bool Equals(object obj)
        {
            return obj is AttributeId other && Equals(other);
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

        public static explicit operator AttributeId<TNumber>(AttributeId attributeId)
        {
            return new AttributeId<TNumber>(attributeId);
        }

        public static implicit operator AttributeId(AttributeId<TNumber> attributeId)
        {
            return attributeId._id;
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