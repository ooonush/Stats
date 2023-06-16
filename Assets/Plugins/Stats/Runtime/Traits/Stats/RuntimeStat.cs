using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    public sealed class RuntimeStat : IRuntimeStat
    {
        private readonly Modifiers _modifiers;
        private readonly Traits _traits;
        private readonly StatFormula _formula;

        public IReadOnlyList<Modifier> ConstantModifiers => _modifiers.Constants;
        public IReadOnlyList<Modifier> PercentageModifiers => _modifiers.Constants;

        public StatType StatType { get; }

        private float _base;

        public float Base
        {
            get => _base;
            set
            {
                if (Mathf.Abs(_base - value) < float.Epsilon) return;
                SetBase(value);
            }
        }

        private bool _initialized;
        private float _value;

        public float Value
        {
            get
            {
                if (!_initialized)
                {
                    InitializeStartValues();
                }

                return _value;
            }
            private set => _value = value;
        }

        public float ModifiersValue
        {
            get
            {
                float value = _formula ? _formula.Calculate(this, _traits) : _base;
                return _modifiers.Calculate(value) - value;
            }
        }

        public event StatValueChangedAction OnValueChanged;

        public RuntimeStat(Traits traits, StatItem statItem)
        {
            _traits = traits;
            StatType = statItem.StatType;
            _modifiers = new Modifiers();

            _base = statItem.Base;
            _formula = statItem.Formula;

            _traits.RuntimeAttributes.OnValueChanged += (_, _) => RecalculateValue();
        }

        internal void InitializeStartValues()
        {
            _initialized = true;
            _value = CalculateValue();
        }

        private void RecalculateValue()
        {
            float prevValue = Value;
            float nextValue = CalculateValue();
            
            if (Mathf.Abs(prevValue - nextValue) > float.Epsilon)
            {
                Value = nextValue;

                foreach (RuntimeStat runtimeStat in _traits.RuntimeStats)
                {
                    if (runtimeStat.StatType != StatType)
                    {
                        runtimeStat.RecalculateValue();
                    }
                }

                OnValueChanged?.Invoke(StatType, Value - prevValue);
            }
        }

        private float CalculateValue()
        {
            float value = _formula ? _formula.Calculate(this, _traits) : _base;
            float nextValue = _modifiers.Calculate(value);
            return nextValue;
        }

        private void SetBase(float value)
        {
            _base = value;
            RecalculateValue();
        }

        public void AddModifier(ModifierType modifierType, float value)
        {
            _modifiers.Add(modifierType, value);
            RecalculateValue();
        }

        public bool RemoveModifier(ModifierType modifierType, float value)
        {
            bool success = _modifiers.Remove(modifierType, value);

            if (success) RecalculateValue();
            return success;
        }
    }
}
