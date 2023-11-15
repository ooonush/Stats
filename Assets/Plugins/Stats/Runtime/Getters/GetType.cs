using System;

namespace Stats
{
    [Serializable]
    public abstract class GetType<T>
    {
        public abstract T Get();
    }
}