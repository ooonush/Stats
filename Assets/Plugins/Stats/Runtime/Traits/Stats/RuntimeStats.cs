using System;
using System.Collections;
using System.Collections.Generic;
using Type = System.Type;

namespace Stats
{
    public sealed class RuntimeStats : IRuntimeStats
    {
        private readonly Traits _traits;
        private readonly Dictionary<string, IRuntimeStatBase> _stats = new();

        public int Count => _stats.Count;

        internal RuntimeStats(Traits traits)
        {
            _traits = traits;
        }

        internal void SyncWithTraitsClass(ITraitsClass traitsClass)
        {
            _stats.Clear();
            
            foreach ((string statId, object stat) in traitsClass.StatItems)
            {
                foreach (Type statInterface in stat.GetType().GetInterfaces())
                {
                    if (statInterface.IsGenericType && statInterface.GetGenericTypeDefinition() == typeof(IStat<>))
                    {
                        Type genericStatNumberType = statInterface.GenericTypeArguments[0];
                        Type runtimeStat = typeof(RuntimeStat<>).MakeGenericType(genericStatNumberType);
                        
                        object genericRuntimeStat = Activator.CreateInstance(runtimeStat, _traits, stat);
                        
                        if (_stats.ContainsKey(statId))
                        {
                            throw new Exception($"Stat with id \"{statId}\" already exists");
                        }
                        
                        _stats[statId] = (IRuntimeStatBase)genericRuntimeStat;
                    }
                }
            }
        }

        IRuntimeStat<TNumber> IRuntimeStats.Get<TNumber>(StatId<TNumber> statId) => Get(statId);

        public RuntimeStat<TNumber> Get<TNumber>(StatId<TNumber> statId) where TNumber : IStatNumber<TNumber>
        {
            return (RuntimeStat<TNumber>)_stats[statId];
        }

        public bool Contains<TNumber>(StatId<TNumber> statId) where TNumber : IStatNumber<TNumber>
        {
            return _stats.ContainsKey(statId);
        }

        public IEnumerator<IRuntimeStat> GetEnumerator() => _stats.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}