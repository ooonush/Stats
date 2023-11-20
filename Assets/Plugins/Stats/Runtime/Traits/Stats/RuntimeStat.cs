using System;
using System.Collections.Generic;

namespace Stats
{
    public sealed class RuntimeStat<TNumber> : IRuntimeStatBase, IRuntimeStat<TNumber> where TNumber : IStatNumber<TNumber>
    {
        private readonly Modifiers<TNumber> _modifiers = new Modifiers<TNumber>();
        private readonly Traits _traits;
        private readonly StatFormula<TNumber> _formula;

        public IReadOnlyList<ConstantModifier<TNumber>> ConstantModifiers => _modifiers.Constants;
        public IReadOnlyList<PercentageModifier> PercentageModifiers => _modifiers.Percentages;

        public StatId<TNumber> StatId { get; }
        string IRuntimeStat.StatId => StatId;

        public TNumber Base
        {
            get => _base;
            set
            {
                if (_base.Equals(value)) return;
                SetBase(value);
            }
        }

        public TNumber Value
        {
            get
            {
                if (!_initialized)
                {
                    ((IRuntimeStatBase)this).InitializeStartValues();
                }

                return _value;
            }
            private set => _value = value;
        }

        public TNumber ModifiersValue
        {
            get
            {
                TNumber value = _formula ? _formula.Calculate(this, _traits) : _base;
                return _modifiers.Calculate(value).Subtract(value);
            }
        }

        private TNumber _base;
        private TNumber _value;
        private bool _initialized;

        private event Action OnChanged;
        event Action IRuntimeStat.OnChanged
        {
            add => OnChanged += value;
            remove => OnChanged -= value;
        }

        public event StatValueChangedAction<TNumber> OnValueChanged;

        public RuntimeStat(Traits traits, IStat<TNumber> stat)
        {
            _traits = traits;
            StatId = stat.StatId;
            _base = stat.Base;
            _formula = stat.Formula;
        }

        void IRuntimeStatBase.InitializeStartValues()
        {
            _initialized = true;
            _value = CalculateValue();
        }

        void IRuntimeStatBase.RecalculateValue()
        {
            TNumber prevValue = Value;
            TNumber nextValue = CalculateValue();

            if (prevValue.Equals(nextValue)) return;

            Value = nextValue;

            foreach (IRuntimeStat runtimeStat in _traits.RuntimeStats)
            {
                if (runtimeStat != this)
                {
                    ((IRuntimeStatBase)runtimeStat).RecalculateValue();
                }
            }

            OnChanged?.Invoke();
            OnValueChanged?.Invoke(StatId, prevValue, Value);
        }

        private TNumber CalculateValue()
        {
            TNumber value = _formula ? _formula.Calculate(this, _traits) : _base;
            return _modifiers.Calculate(value);
        }

        private void SetBase(TNumber value)
        {
            _base = value;
            ((IRuntimeStatBase)this).RecalculateValue();
        }

        public void AddModifier(ConstantModifier<TNumber> modifier)
        {
            _modifiers.Add(modifier);
            ((IRuntimeStatBase)this).RecalculateValue();
        }

        public void AddModifier(PercentageModifier modifier)
        {
            _modifiers.Add(modifier);
            ((IRuntimeStatBase)this).RecalculateValue();
        }

        public bool RemoveModifier(ConstantModifier<TNumber> modifier)
        {
            bool success = _modifiers.Remove(modifier);

            if (success) ((IRuntimeStatBase)this).RecalculateValue();
            return success;
        }

        public bool RemoveModifier(PercentageModifier modifier)
        {
            bool success = _modifiers.Remove(modifier);

            if (success) ((IRuntimeStatBase)this).RecalculateValue();
            return success;
        }

        public override string ToString() => $"{StatId}: {Value}";
    }
}