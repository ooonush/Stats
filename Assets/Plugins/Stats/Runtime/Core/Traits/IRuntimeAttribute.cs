namespace Stats
{
    public interface IRuntimeAttribute
    {
        float MinValue { get; }
        float MaxValue { get; }
        float Value { get; set; }
        /// <summary>
        /// <see cref="Value"/> to <see cref="MaxValue"/> ratio
        /// </summary>
        float Ratio { get; }
        AttributeType AttributeType { get; }
        event AttributeValueChangedAction OnValueChanged;
    }
}