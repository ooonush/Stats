using System;

namespace Stats
{
    public abstract class RuntimeAttribute
    {
        internal event Action OnValueChanged;
        internal abstract void InitializeStartValues();

        protected void InvokeOnValueChanged() => OnValueChanged?.Invoke();
    }

    public sealed class RuntimeAttribute<TNumber> : RuntimeAttribute, IRuntimeAttribute<TNumber> where TNumber : IStatNumber<TNumber>
    {
        private readonly Traits _traits;
        public AttributeId<TNumber> AttributeId => _attribute.AttributeId;

        public RuntimeStat<TNumber> MaxRuntimeStat => _attribute.MaxValueStat != null
            ? _traits.RuntimeStats.Get(_attribute.MaxValueStat.StatId)
            : default;

        public TNumber MinValue { get; }

        private bool _initialized;
        private TNumber _value;
        private readonly IAttribute<TNumber> _attribute;

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
            set
            {
                TNumber oldValue = Value;
                TNumber newValue = TMath.Clamp(value, MinValue, MaxRuntimeStat.Value);
                if (!_value.Equals(newValue)) return;

                _value = newValue;
                OnValueChanged?.Invoke(AttributeId, newValue.Subtract(oldValue));
                InvokeOnValueChanged();
            }
        }

        public new event AttributeValueChangedAction<TNumber> OnValueChanged;

        public RuntimeAttribute(Traits traits, IAttribute<TNumber> attribute)
        {
            _traits = traits;
            _attribute = attribute;

            MinValue = attribute.MinValue;

            if (attribute.MaxValueStat != null)
            {
                traits.RuntimeStats.Get(attribute.MaxValueStat.StatId).OnValueChanged += (_, _) => OnMaxValueChanged();
            }
        }

        internal override void InitializeStartValues()
        {
            _initialized = true;
            _value = TMath.Lerp(_attribute.StartPercent, MinValue, MaxRuntimeStat.Value);
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