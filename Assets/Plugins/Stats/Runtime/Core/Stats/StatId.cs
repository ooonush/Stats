using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct StatId<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private string _id;

        public StatId(string id)
        {
            _id = id;
        }

        public static bool operator ==(StatId<TNumber> left, StatId<TNumber> right)
        {
            return left._id == right._id;
        }

        public static bool operator !=(StatId<TNumber> a, StatId<TNumber> b)
        {
            return !(a == b);
        }

        public static implicit operator string(StatId<TNumber> identifier)
        {
            return identifier._id;
        }

        public static implicit operator StatId<TNumber>(string id)
        {
            return new StatId<TNumber>(id);
        }

        public bool Equals(StatId<TNumber> other)
        {
            return _id == other._id;
        }

        public override bool Equals(object obj)
        {
            return obj is StatId<TNumber> other && Equals(other);
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