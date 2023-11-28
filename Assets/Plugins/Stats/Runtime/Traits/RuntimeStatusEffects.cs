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
            if (_effects.TryGetValue(statusEffect.Id, out LinkedList<StatusEffect> activeEffects))
            {
                while (activeEffects.Count >= statusEffect.MaxStack)
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

            foreach (LinkedList<StatusEffect> activeEffects in _effects.Values)
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
            LinkedList<StatusEffect> activeEffects = _effects[statusEffect.Id];
            LinkedListNode<StatusEffect> activeEffectNode = activeEffects.Find(statusEffect);
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
                LinkedList<StatusEffect> activeEffects = _effects[statusEffectId];
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