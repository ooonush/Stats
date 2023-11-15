using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Type = System.Type;

namespace Stats
{
    public sealed class RuntimeStats : IRuntimeStats, IEnumerable<RuntimeStat>
    {
        private readonly Traits _traits;
        private readonly Dictionary<string, RuntimeStat> _stats = new();

        public int Count => _stats.Values.Count;

        internal RuntimeStats(Traits traits)
        {
            _traits = traits;
        }
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        internal void SyncWithTraitsClass(ITraitsClass traitsClass)
        {
            ClearStats();
            
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
                        
                        _stats[statId] = (RuntimeStat)genericRuntimeStat;
                    }
                }
            }
            
            // foreach ((string attributeId, object attribute) in traitsClass.AttributeItems)
            // {
            //     foreach (Type attributeInterface in attribute.GetType().GetInterfaces())
            //     {
            //         if (attributeInterface.IsGenericType && attributeInterface.GetGenericTypeDefinition() == typeof(IAttribute<>))
            //         {
            //             object stat = attributeInterface.GetProperty("MaxValueStat")!.GetValue(attribute);
            //             Type genericStatNumberType = attributeInterface.GenericTypeArguments[0];
            //             Type runtimeStat = typeof(RuntimeStat<>).MakeGenericType(genericStatNumberType);
            //
            //             object genericRuntimeStat = Activator.CreateInstance(runtimeStat, _traits, stat);
            //
            //             if (_stats.ContainsKey(attributeId))
            //             {
            //                 throw new Exception($"Stat with id \"{attributeId}\" already exists");
            //             }
            //
            //             _stats[attributeId] = (RuntimeStat)genericRuntimeStat;
            //         }
            //     }
            // }
        }

        private void ClearStats()
        {
            foreach (string statId in _stats.Keys.ToArray())
            {
                _stats.Remove(statId);
            }
        }

        // private RuntimeStat Get(StatIdAsset statIdAsset)
        // {
        //     if (statIdAsset == null) throw new ArgumentNullException(nameof(statIdAsset));
        //
        //     try
        //     {
        //         return _stats[statIdAsset];
        //     }
        //     catch (Exception exception)
        //     {
        //         throw new ArgumentException("StatType not found in RuntimeStats", nameof(statIdAsset), exception);
        //     }
        // }

        IRuntimeStat<TNumber> IRuntimeStats.Get<TNumber>(StatId<TNumber> statId) => Get(statId);

        public RuntimeStat<TNumber> Get<TNumber>(StatId<TNumber> statId) where TNumber : IStatNumber<TNumber>
        {
            return (RuntimeStat<TNumber>)_stats[statId];
        }

        public bool Contains<TNumber>(StatId<TNumber> statId) where TNumber : IStatNumber<TNumber>
        {
            return _stats.ContainsKey(statId);
        }

        // public IEnumerator<RuntimeStat> GetEnumerator() => _stats.Values.GetEnumerator();
        public IEnumerator<RuntimeStat> GetEnumerator()
        {
            return _stats.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}