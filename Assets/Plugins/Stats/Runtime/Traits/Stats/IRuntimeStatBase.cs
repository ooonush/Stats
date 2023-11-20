namespace Stats
{
    internal interface IRuntimeStatBase : IRuntimeStat
    {
        void InitializeStartValues();
        void RecalculateValue();
    }
}