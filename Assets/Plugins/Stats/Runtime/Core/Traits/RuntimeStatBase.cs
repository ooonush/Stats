using System.Collections.Generic;

namespace Stats
{
    public abstract class RuntimeStatBase
    {
        internal abstract void RecalculateValue(bool notify = true);
    }

    public abstract class RuntimeStatBase<TNumber> : RuntimeStatBase, IRuntimeStat<TNumber> where TNumber : IStatNumber<TNumber>
    {
        private readonly ITraits _traits;
        private readonly StatFormula<TNumber> _formula;
        protected readonly Modifiers<TNumber> Modifiers = new();

        public StatId<TNumber> StatId { get; }

        public virtual TNumber Base
        {
            get => _base;
            set
            {
                if (_base.Equals(value)) return;
                _base = value;
                RecalculateValue();
            }
        }

        private TNumber _value;
        private bool _initialized;
        private TNumber _base;

        public TNumber Value
        {
            get
            {
                if (!_initialized)
                {
                    ((IRuntimeStat)this).InitializeStartValues();
                }

                return _value;
            }
            private set => _value = value;
        }

        public TNumber ModifiersValue
        {
            get
            {
                TNumber value = _formula ? _formula.Calculate(this, _traits) : Base;
                return Modifiers.Calculate(value).Subtract(value);
            }
        }

        public IReadOnlyList<PercentageModifier> PercentageModifiers => Modifiers.Percentages;
        public IReadOnlyList<ConstantModifier<TNumber>> ConstantModifiers => Modifiers.Constants;

        public event StatValueChangedAction<TNumber> OnValueChanged;

        #region TDouble

        string IRuntimeStat.StatId => StatId;
        TDouble IRuntimeStat.Base => Base.ToDouble();
        TDouble IRuntimeStat.Value => Value.ToDouble();
        TDouble IRuntimeStat.ModifiersValue => ModifiersValue.ToDouble();
        IReadOnlyList<ConstantModifier<TDouble>> IRuntimeStat.ConstantModifiers => Modifiers.Constants;

        private event StatValueChangedAction OnDoubleValueChanged;

        event StatValueChangedAction IRuntimeStat.OnValueChanged
        {
            add => OnDoubleValueChanged += value;
            remove => OnDoubleValueChanged -= value;
        }

        #endregion

        protected RuntimeStatBase(ITraits traits, IStat<TNumber> stat)
        {
            _traits = traits;
            StatId = stat.StatId;
            _formula = stat.Formula;
            _base = stat.Base;
        }

        protected void SetBaseWithoutNotify(TNumber value)
        {
            _base = value;
            RecalculateValue(false);
        }

        public virtual void AddModifier(ConstantModifier<TNumber> modifier)
        {
            Modifiers.Add(modifier);
            RecalculateValue();
        }

        public virtual void AddModifier(PercentageModifier modifier)
        {
            Modifiers.Add(modifier);
            RecalculateValue();
        }

        public virtual bool RemoveModifier(ConstantModifier<TNumber> modifier)
        {
            bool removed = Modifiers.Remove(modifier);
            RecalculateValue();
            return removed;
        }

        public virtual bool RemoveModifier(PercentageModifier modifier)
        {
            bool removed = Modifiers.Remove(modifier);
            RecalculateValue();
            return removed;
        }

        void IRuntimeStat.InitializeStartValues()
        {
            _initialized = true;
            _value = CalculateValue();
        }

        internal override void RecalculateValue(bool notify = true)
        {
            TNumber prevValue = Value;
            Value = CalculateValue();
            
            if (prevValue.Equals(Value)) return;
            
            foreach (IRuntimeStat runtimeStat in _traits.RuntimeStats)
            {
                if (runtimeStat != this)
                {
                    ((RuntimeStatBase)runtimeStat).RecalculateValue();
                }
            }
            
            if (!notify) return;
            
            InvokeOnValueChanged(prevValue, Value);
        }

        private TNumber CalculateValue()
        {
            TNumber value = _formula ? _formula.Calculate(this, _traits) : Base;
            return Modifiers.Calculate(value);
        }

        private void InvokeOnValueChanged(TNumber prevValue, TNumber nextValue)
        {
            OnValueChanged?.Invoke(StatId, prevValue, nextValue);
            OnDoubleValueChanged?.Invoke(StatId, prevValue.ToDouble(), nextValue.ToDouble());
        }

        public override string ToString() => $"{StatId}: {Value}";
    }
}