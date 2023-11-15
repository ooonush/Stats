using System.Collections.Generic;

namespace Stats
{
    public abstract class RuntimeStat
    {
        protected internal abstract void RecalculateValue();
        internal abstract void InitializeStartValues();
    }

    public sealed class RuntimeStat<TNumber> : RuntimeStat, IRuntimeStat<TNumber> where TNumber : IStatNumber<TNumber>
    {
        private readonly Modifiers<TNumber> _modifiers;
        private readonly Traits _traits;
        private readonly StatFormula<TNumber> _formula;

        public IReadOnlyList<ConstantModifier<TNumber>> ConstantModifiers => _modifiers.Constants;
        public IReadOnlyList<ConstantModifier<TNumber>> PercentageModifiers => _modifiers.Constants;

        public StatId<TNumber> StatId { get; }

        private TNumber _base;

        public TNumber Base
        {
            get => _base;
            set
            {
                if (_base.Equals(value)) return;
                SetBase(value);
            }
        }

        private bool _initialized;
        private TNumber _value;

        public TNumber Value
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

        public TNumber ModifiersValue
        {
            get
            {
                TNumber value = _formula ? _formula.Calculate(this, _traits) : _base;
                return _modifiers.Calculate(value).Subtract(value);
            }
        }

        public event StatValueChangedAction<TNumber> OnValueChanged;

        public RuntimeStat(Traits traits, IStat<TNumber> stat)
        {
            _traits = traits;
            StatId = stat.StatId;
            _modifiers = new Modifiers<TNumber>();
            
            _base = stat.Base;
            _formula = stat.Formula;
            
            _traits.RuntimeAttributes.OnValueChanged += RecalculateValue;
        }

        internal override void InitializeStartValues()
        {
            _initialized = true;
            _value = CalculateValue();
        }

        protected internal override void RecalculateValue()
        {
            TNumber prevValue = Value;
            TNumber nextValue = CalculateValue();

            if (prevValue.Equals(nextValue)) return;

            Value = nextValue;

            foreach (RuntimeStat runtimeStat in _traits.RuntimeStats)
            {
                if (runtimeStat != this)
                {
                    runtimeStat.RecalculateValue();
                }
            }

            OnValueChanged?.Invoke(StatId, Value.Subtract(prevValue));
        }

        private TNumber CalculateValue()
        {
            TNumber value = _formula ? _formula.Calculate(this, _traits) : _base;
            TNumber nextValue = _modifiers.Calculate(value);
            return nextValue;
        }

        private void SetBase(TNumber value)
        {
            _base = value;
            RecalculateValue();
        }

        public void AddModifier(ConstantModifier<TNumber> modifier)
        {
            _modifiers.Add(modifier);
            RecalculateValue();
        }

        public void AddModifier(PercentageModifier modifier)
        {
            _modifiers.Add(modifier);
            RecalculateValue();
        }

        public bool RemoveModifier(ConstantModifier<TNumber> modifier)
        {
            bool success = _modifiers.Remove(modifier);

            if (success) RecalculateValue();
            return success;
        }

        public bool RemoveModifier(PercentageModifier modifier)
        {
            bool success = _modifiers.Remove(modifier);

            if (success) RecalculateValue();
            return success;
        }

        public override string ToString()
        {
            return $"{StatId}: {Value}";
        }
    }
}
