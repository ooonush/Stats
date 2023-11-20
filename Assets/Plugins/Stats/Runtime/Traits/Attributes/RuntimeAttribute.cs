using System;

namespace Stats
{
    public sealed class RuntimeAttribute<TNumber> : IRuntimeAttributeBase, IRuntimeAttribute<TNumber> where TNumber : IStatNumber<TNumber>
    {
        public AttributeId<TNumber> AttributeId { get; }

        IRuntimeStat<TNumber> IRuntimeAttribute<TNumber>.MaxRuntimeStat => MaxRuntimeStat;

        public RuntimeStat<TNumber> MaxRuntimeStat { get; }

        public TNumber MinValue { get; }

        public TNumber Value
        {
            get
            {
                if (!_initialized)
                {
                    ((IRuntimeAttributeBase)this).InitializeStartValues();
                }
                
                return _value;
            }
            set
            {
                TNumber oldValue = Value;
                TNumber newValue = TMath.Clamp(value, MinValue, MaxRuntimeStat.Value);
                if (!_value.Equals(newValue)) return;
                
                _value = newValue;
                OnChanged?.Invoke();
                OnValueChanged?.Invoke(AttributeId, oldValue, newValue);
                foreach (IRuntimeStat runtimeStat in _traits.RuntimeStats)
                {
                    ((IRuntimeStatBase)runtimeStat).RecalculateValue();
                }
            }
        }

        private readonly float _startPercent;

        private bool _initialized;

        private TNumber _value;

        private Traits _traits;

        string IRuntimeAttribute.AttributeId => AttributeId;

        public event AttributeValueChangedAction<TNumber> OnValueChanged;
        private event Action OnChanged;
        event Action IRuntimeAttribute.OnValueChanged
        {
            add => OnChanged += value;
            remove => OnChanged -= value;
        }

        void IRuntimeAttributeBase.InitializeStartValues()
        {
            _initialized = true;
            _value = TMath.Lerp(MinValue, MaxRuntimeStat.Value, _startPercent);
        }

        public RuntimeAttribute(Traits traits, IAttribute<TNumber> attribute)
        {
            MinValue = attribute.MinValue;
            AttributeId = attribute.AttributeId;
            MaxRuntimeStat = traits.RuntimeStats.Get(attribute.MaxValueStat.StatId);
            
            _traits = traits;
            _startPercent = attribute.StartPercent;
            
            MaxRuntimeStat.OnValueChanged += (_, _, _) => OnMaxValueChanged();
        }

        private void OnMaxValueChanged()
        {
            TNumber value = TMath.Clamp(_value, MinValue, MaxRuntimeStat.Value);
            if (!Value.Equals(value))
            {
                Value = value;
            }
        }

        public override string ToString()
        {
            return $"{AttributeId}: {Value}";
        }
    }
}