using System;

namespace Stats
{
    [Serializable]
    public abstract class TraitsClassGetType : IGetType<ITraitsClass>
    {
        public abstract ITraitsClass Get();
    }
}