using UnityEngine;

namespace Stats
{
    public abstract class StatusEffect : IdScriptableObject
    {
        [SerializeField] private bool _limitStack;
        public bool LimitStack => _limitStack;
        [SerializeField, Min(1)] private int _maxStack = 1;
        public int MaxStack => _limitStack ? _maxStack : int.MaxValue;

        public virtual void OnStart(ITraits traits) { }

        public virtual bool OnUpdate(ITraits traits) => false;

        public virtual void OnEnd(ITraits traits) { }
    }
}