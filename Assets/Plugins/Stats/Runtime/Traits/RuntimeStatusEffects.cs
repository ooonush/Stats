using System.Collections.Generic;

namespace Stats
{
    public sealed class RuntimeStatusEffects
    {
        private readonly Dictionary<string, LinkedList<StatusEffect>> _effects = new();
        private readonly ITraits _traits;

        public RuntimeStatusEffects(ITraits traits) => _traits = traits;

        public void Add(StatusEffect statusEffect)
        {
            if (_effects.TryGetValue(statusEffect.Id, out var activeEffects))
            {
                while (_effects.Count >= statusEffect.MaxStack)
                {
                    activeEffects.First.Value.OnEnd(_traits);
                    activeEffects.RemoveFirst();
                }
            }
            else
            {
                activeEffects = new LinkedList<StatusEffect>();
                _effects.Add(statusEffect.Id, activeEffects);
            }

            StatusEffect activeEffect = UnityEngine.Object.Instantiate(statusEffect);
            activeEffects.AddLast(activeEffect);
            activeEffect.OnStart(_traits);
        }

        public void Update()
        {
            var effectIdsToRemove = new List<StatusEffect>();

            foreach (var activeEffects in _effects.Values)
            {
                foreach (StatusEffect activeEffect in activeEffects)
                {
                    if (!activeEffect.OnUpdate(_traits))
                    {
                        effectIdsToRemove.Add(activeEffect);
                    }
                }
            }

            foreach (StatusEffect statusEffect in effectIdsToRemove)
            {
                Remove(statusEffect);
            }
        }

        public void Remove(StatusEffect statusEffect)
        {
            var activeEffects = _effects[statusEffect.Id];
            var activeEffectNode = activeEffects.Find(statusEffect);
            if (activeEffectNode != null)
            {
                activeEffectNode.Value.OnEnd(_traits);
                activeEffects.Remove(activeEffectNode);
            }
            if (activeEffects.Count == 0)
            {
                _effects.Remove(statusEffect.Id);
            }
        }

        public void Clear()
        {
            foreach (string statusEffectId in _effects.Keys)
            {
                var activeEffects = _effects[statusEffectId];
                foreach (StatusEffect activeEffect in activeEffects)
                {
                    activeEffect.OnEnd(_traits);
                }
                activeEffects.Clear();
            }
            _effects.Clear();
        }
    }
}