using System;
using System.Collections;
using System.Collections.Generic;

namespace Stats
{
    [Serializable]
    public class ModifierList<TModifier, TNumber> : IReadOnlyList<TModifier>
        where TModifier : struct, IModifier<TNumber>
        where TNumber : IStatNumber<TNumber>
    {
        internal TNumber PositiveValue { get; private set; }
        internal TNumber NegativeValue { get; private set; }
        public int Count => _list.Count;
        public TModifier this[int index] => _list[index];

        private readonly List<TModifier> _list = new();

        public void Add(TModifier modifier)
        {
            switch (modifier.ModifierType)
            {
                case ModifierType.Positive:
                    PositiveValue = PositiveValue.Add(modifier.Value);
                    break;
                case ModifierType.Negative:
                    NegativeValue = NegativeValue.Add(modifier.Value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _list.Add(modifier);
        }

        public bool Remove(TModifier modifier)
        {
            for (int i = _list.Count - 1; i >= 0; --i)
            {
                if (_list[i].Value.Equals(modifier.Value) && _list[i].ModifierType == modifier.ModifierType)
                {
                    switch (modifier.ModifierType)
                    {
                        case ModifierType.Positive:
                            PositiveValue = PositiveValue.Subtract(modifier.Value);
                            break;
                        case ModifierType.Negative:
                            NegativeValue = NegativeValue.Subtract(modifier.Value);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    
                    _list.RemoveAt(i);
                    return true;
                }
            }
            
            return false;
        }

        public void Clear() => _list.Clear();
        public IEnumerator GetEnumerator() => ((IEnumerable)_list).GetEnumerator();
        IEnumerator<TModifier> IEnumerable<TModifier>.GetEnumerator() => _list.GetEnumerator();
    }
}