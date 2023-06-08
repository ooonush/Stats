using System;
using System.Collections;
using System.Collections.Generic;

namespace Stats
{
    public sealed class RuntimeStats : IRuntimeStats<RuntimeStat>
    {
        private readonly Traits _traits;
        private readonly Dictionary<StatType, RuntimeStat> _stats = new();

        public int Count => _stats.Values.Count;

        public event StatValueChangedAction OnValueChanged;

        internal RuntimeStats(Traits traits)
        {
            _traits = traits;
        }

        internal void SyncWithTraitsClass(TraitsClassBase traitsClass)
        {
            ClearStats();

            foreach (StatItem statItem in traitsClass.StatItems)
            {
                if (statItem == null || !statItem.StatType)
                {
                    throw new NullReferenceException("No StatType reference found in TraitsClass");
                }

                StatType statType = statItem.StatType;
                if (_stats.ContainsKey(statType))
                {
                    throw new Exception($"Stat with StatType \"{statType.name}\" already exists");
                }

                var runtimeStat = new RuntimeStat(_traits, statItem);

                runtimeStat.OnValueChanged += InvokeOnValueChanged;
                _stats[statType] = runtimeStat;
            }
        }

        private void InvokeOnValueChanged(StatType statType, float change)
        {
            OnValueChanged?.Invoke(statType, change);
        }

        private void ClearStats()
        {
            var runtimeAttributes = new List<RuntimeStat>(_stats.Values);
            foreach (RuntimeStat runtimeStat in runtimeAttributes)
            {
                runtimeStat.OnValueChanged -= InvokeOnValueChanged;
                _stats.Remove(runtimeStat.StatType);
            }
        }

        public RuntimeStat Get(StatType statType)
        {
            if (statType == null) throw new ArgumentNullException(nameof(statType));

            try
            {
                return _stats[statType];
            }
            catch (Exception exception)
            {
                throw new ArgumentException("StatType not found in RuntimeStats", nameof(statType), exception);
            }
        }

        public bool Contains(StatType statType) => _stats.ContainsKey(statType);

        public IEnumerator<RuntimeStat> GetEnumerator() => _stats.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}