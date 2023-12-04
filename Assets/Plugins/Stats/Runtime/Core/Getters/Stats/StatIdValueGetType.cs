using System;
using AInspector;
using UnityEngine;

namespace Stats
{
    [Serializable]
    [DropdownName("String")]
    internal class StatIdValueGetType : StatIdGetType
    {
        [SerializeField] private StatId _statId;
        public override StatId Get() => _statId;

        public StatIdValueGetType()
        {
        }
        
        public StatIdValueGetType(StatId statId)
        {
            _statId = statId;
        }
    }

    [Serializable]
    [DropdownName("String")]
    internal abstract class StatIdValueGetType<TNumber> : StatIdGetType<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private StatId<TNumber> _statId;
        public override StatId<TNumber> Get() => _statId;
    }
}