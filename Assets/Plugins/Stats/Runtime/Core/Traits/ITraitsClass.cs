using System.Collections.Generic;

namespace Stats
{
    public interface ITraitsClass
    {
        string Id { get; }
        IReadOnlyList<IStat> Stats { get; }
        IReadOnlyList<IAttribute> Attributes { get; }
    }
}