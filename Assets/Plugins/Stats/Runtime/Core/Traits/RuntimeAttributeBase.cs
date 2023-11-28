namespace Stats
{
    public abstract class RuntimeAttributeBase<TNumber> : IRuntimeAttribute<TNumber> where TNumber : IStatNumber<TNumber>
    {
        private readonly ITraits _traits;
        private readonly float _startPercent;
        private bool _initialized;
        private TNumber _value;

        public AttributeId<TNumber> AttributeId { get; }

        public TNumber MinValue { get; }

        public virtual TNumber Value
        {
            get
            {
                if (!_initialized)
                {
                    ((IRuntimeAttribute)this).InitializeStartValues();
                }
                
                return _value;
            }
            set
            {
                TNumber prevValue = _value;
                _value = TMath.Clamp(value, MinValue, MaxRuntimeStat.Value);
                if (_value.Equals(prevValue)) return;
                
                InvokeOnValueChanged(prevValue, _value);
                RecalculateValue();
            }
        }

        public IRuntimeStat<TNumber> MaxRuntimeStat { get; }

        string IRuntimeAttribute.AttributeId => AttributeId;

        TDouble IRuntimeAttribute.MinValue => MinValue.ToDouble();

        TDouble IRuntimeAttribute.Value => Value.ToDouble();

        IRuntimeStat IRuntimeAttribute.MaxRuntimeStat => MaxRuntimeStat;

        public event AttributeValueChangedAction<TNumber> OnValueChanged;

        private event AttributeValueChangedAction OnDoubleValueChanged;

        event AttributeValueChangedAction IRuntimeAttribute.OnValueChanged
        {
            add => OnDoubleValueChanged += value;
            remove => OnDoubleValueChanged -= value;
        }

        protected RuntimeAttributeBase(ITraits traits, IAttribute<TNumber> attribute)
        {
            _traits = traits;
            MinValue = attribute.MinValue;
            AttributeId = attribute.AttributeId;
            MaxRuntimeStat = traits.RuntimeStats.Get(attribute.MaxValueStat.StatId);

            _startPercent = attribute.StartPercent;

            MaxRuntimeStat.OnValueChanged += (_, _, _) => OnMaxValueChanged();
        }

        private void InvokeOnValueChanged(TNumber prevValue, TNumber nextValue)
        {
            OnDoubleValueChanged?.Invoke(AttributeId, prevValue.ToDouble(), nextValue.ToDouble());
            OnValueChanged?.Invoke(AttributeId, prevValue, nextValue);
        }

        private void RecalculateValue(bool notify = true)
        {
            foreach (IRuntimeStat runtimeStat in _traits.RuntimeStats)
            {
                ((RuntimeStatBase)runtimeStat).RecalculateValue(notify);
            }
        }

        private void SetValueWithoutNotify(TNumber value)
        {
            TNumber prevValue = _value;
            _value = TMath.Clamp(value, MinValue, MaxRuntimeStat.Value);
            if (_value.Equals(prevValue)) return;
            
            RecalculateValue(false);
        }

        void IRuntimeAttribute.InitializeStartValues()
        {
            _initialized = true;
            _value = TMath.Lerp(MinValue, MaxRuntimeStat.Value, _startPercent);
        }

        private void OnMaxValueChanged()
        {
            TNumber value = TMath.Clamp(_value, MinValue, MaxRuntimeStat.Value);
            if (!Value.Equals(value))
            {
                SetValueWithoutNotify(value);
            }
        }

        public override string ToString() => $"{AttributeId}: {Value}";
    }
}