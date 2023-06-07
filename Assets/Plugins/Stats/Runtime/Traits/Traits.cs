using System;
using UnityEngine;

namespace Stats
{
    [AddComponentMenu("Stats/Traits")]
    public sealed class Traits : MonoBehaviour, ITraits
    {
        [SerializeField] private TraitsClassBase _traitsClass;

        private RuntimeStats _runtimeStats;
        private RuntimeAttributes _runtimeAttributes;
        private bool _initialized;

        public RuntimeStats RuntimeStats => _runtimeStats;
        public RuntimeAttributes RuntimeAttributes => _runtimeAttributes;
        IRuntimeStats<IRuntimeStat> ITraits.RuntimeStats => RuntimeStats;
        IRuntimeAttributes<IRuntimeAttribute> ITraits.RuntimeAttributes => RuntimeAttributes;

        private void Awake()
        {
            _runtimeStats = new RuntimeStats(this);
            _runtimeAttributes = new RuntimeAttributes(this);

            if (_traitsClass)
            {
                _runtimeStats.SyncWithTraitsClass(_traitsClass);
                _runtimeAttributes.SyncWithTraitsClass(_traitsClass);
            }
        }

        private void Start()
        {
            const string error = "Traits not initialized. Please set TraitsClass in the inspector or call Initialize()";
            if (!_traitsClass)
            {
                Debug.LogError(error);
            }
            _initialized = true;
        }

        public void Initialize(TraitsClassBase traitsClass)
        {
            if (_initialized)
            {
                throw new InvalidOperationException("Traits already initialized");
            }

            if (!traitsClass)
            {
                throw new ArgumentNullException(nameof(traitsClass), "TraitsClass cannot be null");
            }

            _initialized = true;
            _traitsClass = traitsClass;
            SyncWithTraitsClass(traitsClass);
        }

        private void SyncWithTraitsClass(TraitsClassBase traitsClass)
        {
            _runtimeStats.SyncWithTraitsClass(traitsClass);
            _runtimeAttributes.SyncWithTraitsClass(traitsClass);

            foreach (RuntimeStat runtimeStat in _runtimeStats)
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