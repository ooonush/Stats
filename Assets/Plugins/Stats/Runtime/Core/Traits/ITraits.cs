namespace Stats
{
    public interface ITraits
    {
        public IRuntimeStats RuntimeStats { get; }
        public IRuntimeAttributes RuntimeAttributes { get; }
    }
}