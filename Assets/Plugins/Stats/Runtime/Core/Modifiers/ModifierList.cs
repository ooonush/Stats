using System;
using System.Collections;
using System.Collections.Generic;

namespace Stats
{
    [Serializable]
    public sealed class ModifierList : IReadOnlyList<Modifier>
    {
        private readonly List<Modifier> _list = new();

        public float Value { get; private set; }

        public int Count => _list.Count;

        public Modifier this[int index] => _list[index];

        public Modifier Add(float value)
        {
            Value += value;
            var modifier = new Modifier(value);
            _list.Add(modifier);

            return modifier;
        }

        public bool Remove(float value)
        {
            for (int i = _list.Count - 1; i >= 0; --i)
            {
                if (Math.Abs(_list[i].Value - value) >= float.Epsilon) continue;
                Value -= _list[i].Value;
                _list.RemoveAt(i);

                return true;
            }

            return false;
        }

        public void Clear() => _list.Clear();

        IEnumerator<Modifier> IEnumerable<Modifier>.GetEnumerator() => _list.GetEnumerator();

        public IEnumerator GetEnumerator() => ((IEnumerable)_list).GetEnumerator();
    }
}