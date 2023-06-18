using System.Collections.Generic;

namespace Stats
{
    public sealed class RuntimeStatusEffects
    {
        private readonly Dictionary<string, Stack<StatusEffect>> _effects = new();
        private readonly ITraits _traits;

        public RuntimeStatusEffects(ITraits traits) => _traits = traits;

        public void Add(StatusEffect statusEffect)
        {
            if (_effects.TryGetValue(statusEffect.Id, out var activeEffects))
            {
                while (_effects.Count >= statusEffect.MaxStack)
                {
                    activeEffects.Pop().OnEnd(_traits);
                }
            }
            else
            {
                activeEffects = new Stack<StatusEffect>();
                _effects.Add(statusEffect.Id, activeEffects);
            }

            StatusEffect activeEffect = UnityEngine.Object.Instantiate(statusEffect);
            activeEffects.Push(activeEffect);
            activeEffect.OnStart(_traits);
        }

        public void Update()
        {
            foreach ((string id, var activeEffects) in _effects)
            {
                foreach (StatusEffect activeEffect in activeEffects)
                {
                    if (!activeEffect.OnUpdate(_traits))
                    {
                        Remove(activeEffect);
                    }
                }
            }
        }

        public void Remove(StatusEffect statusEffect) => Remove(statusEffect.Id);

        public void Clear()
        {
            foreach (string statusEffectId in _effects.Keys)
            {
                Remove(statusEffectId);
            }
        }

        private void Remove(string statusEffectId)
        {
            var activeEffects = _effects[statusEffectId];
            activeEffects.Pop().OnEnd(_traits);
            if (activeEffects.Count == 0)
            {
                _effects.Remove(statusEffectId);
            }
        }
    }
}