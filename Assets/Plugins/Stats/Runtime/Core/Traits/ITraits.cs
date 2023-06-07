namespace Stats
{
    public interface ITraits
    {
        public IRuntimeStats<IRuntimeStat> RuntimeStats { get; }
        public IRuntimeAttributes<IRuntimeAttribute> RuntimeAttributes { get; }
    }
}