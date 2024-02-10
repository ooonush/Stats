using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Stats
{
    public abstract class RuntimeStatsBase : IRuntimeStats
    {
        private static readonly MethodInfo AddStatGenericMethodDefinition;
        private readonly Dictionary<string, IRuntimeStat> _stats = new();
        protected readonly ITraits Traits;

        public int Count => _stats.Count;

        static RuntimeStatsBase()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            Type[] types = { typeof(IStat<>) };
            MethodInfo methodInfo = typeof(RuntimeStatsBase).GetGenericMethod(nameof(AddStat), flags, types);
            AddStatGenericMethodDefinition = methodInfo!.GetGenericMethodDefinition();
        }

        protected RuntimeStatsBase(ITraits traits)
        {
            Traits = traits;
        }

        void IRuntimeStats.SyncWithTraitsClass(ITraitsClass traitsClass)
        {
            Clear();

            foreach (IStat stat in traitsClass.Stats)
            {
                foreach (Type statInterface in stat.GetType().GetInterfaces())
                {
                    if (!statInterface.IsGenericType) continue;
                    if (statInterface.GetGenericTypeDefinition() != typeof(IStat<>)) continue;
                    
                    Type genericStatNumberType = statInterface.GenericTypeArguments[0];
                    
                    MethodInfo methodInfo = AddStatGenericMethodDefinition.MakeGenericMethod(genericStatNumberType);
                    methodInfo.Invoke(this, new object[] { stat });
                }
            }
        }

        void IRuntimeStats.InitializeStartValues()
        {
            foreach (IRuntimeStat runtimeStat in this)
            {
                runtimeStat.InitializeStartValues();
            }
        }

        private void AddStat<TNumber>(IStat<TNumber> stat) where TNumber : struct, IStatNumber<TNumber>
        {
            IRuntimeStat runtimeStat = CreateRuntimeStat(stat);
            string statId = runtimeStat.StatId;
            if (!_stats.TryAdd(statId, runtimeStat))
            {
                throw new Exception($"Stat with id \"{statId}\" already exists");
            }
        }

        protected abstract IRuntimeStat CreateRuntimeStat<TNumber>(IStat<TNumber> stat)
            where TNumber : IStatNumber<TNumber>;

        public bool Contains(StatId statId)
        {
            return _stats.ContainsKey(statId);
        }

        public IRuntimeStat Get(StatId statId)
        {
            return _stats[statId];
        }

        public IRuntimeStat<TNumber> Get<TNumber>(StatId<TNumber> statId) where TNumber : IStatNumber<TNumber>
        {
            return (IRuntimeStat<TNumber>)Get((string)statId);
        }

        protected void Clear()
        {
            _stats.Clear();
        }

        public IEnumerator<IRuntimeStat> GetEnumerator() => _stats.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}