namespace Stats
{
    public interface IRuntimeAttribute
    {
        float MinValue { get; }
        float MaxValue { get; }
        float Value { get; set; }
        float Ratio { get; }
        AttributeType AttributeType { get; }
        event AttributeValueChangedAction OnValueChanged;
    }
}