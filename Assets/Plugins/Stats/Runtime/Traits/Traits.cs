using System;
using UnityEngine;

namespace Stats
{
    [AddComponentMenu("Stats/Traits")]
    [DefaultExecutionOrder(-short.MinValue)]
    public sealed class Traits : MonoBehaviour, ITraits
    {
        [SerializeField] private TraitsClassBase _traitsClass;

        public RuntimeStats RuntimeStats { get; private set; }
        public RuntimeAttributes RuntimeAttributes { get; private set; }
        public RuntimeStatusEffects RuntimeStatusEffects { get; private set; }

        IRuntimeStats<IRuntimeStat> ITraits.RuntimeStats => RuntimeStats;
        IRuntimeAttributes<IRuntimeAttribute> ITraits.RuntimeAttributes => RuntimeAttributes;

        private bool _initialized;

        private void Awake()
        {
            RuntimeStats = new RuntimeStats(this);
            RuntimeAttributes = new RuntimeAttributes(this);
            RuntimeStatusEffects = new RuntimeStatusEffects(this);

            if (_traitsClass)
            {
                Initialize(_traitsClass);
            }
        }

        private void Start()
        {
            const string error = "Traits not initialized. Please set TraitsClass in the inspector or call Initialize()";
            if (!_traitsClass)
            {
                Debug.LogWarning(error);
            }
        }

        public void Initialize(TraitsClassBase traitsClass)
        {
            if (_initialized)
            {
                throw new InvalidOperationException(
                    "Traits already initialized. You must have already called Initialize() or specified TraitsClass in the Inspector.");
            }

            if (!traitsClass)
            {
                throw new ArgumentNullException(nameof(traitsClass), "TraitsClass cannot be null.");
            }

            SyncWithTraitsClass(traitsClass);
            _traitsClass = traitsClass;
            _initialized = true;
        }

        private void OnDestroy()
        {
            RuntimeStatusEffects.Clear();
        }

        private void SyncWithTraitsClass(TraitsClassBase traitsClass)
        {
            RuntimeStats.SyncWithTraitsClass(traitsClass);
            RuntimeAttributes.SyncWithTraitsClass(traitsClass);

            foreach (RuntimeStat runtimeStat in RuntimeStats)
            {
                runtimeStat.InitializeStartValues();
            }

            foreach (RuntimeAttribute runtimeAttribute in RuntimeAttributes)
            {
                runtimeAttribute.InitializeStartValues();
            }
        }
    }
}