namespace Stats
{
    public static class TraitsExtensions
    {
        public static void SyncWithTraitsClass(this ITraits traits, ITraitsClass traitsClass)
        {
            traits.RuntimeStats.SyncWithTraitsClass(traitsClass);
            traits.RuntimeAttributes.SyncWithTraitsClass(traitsClass);
            
            traits.RuntimeStats.InitializeStartValues();
            traits.RuntimeAttributes.InitializeStartValues();
        }
    }
}