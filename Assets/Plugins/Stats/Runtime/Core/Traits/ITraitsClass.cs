using System.Collections.Generic;

namespace Stats
{
    public interface ITraitsClass
    {
        string Id { get; }
        IReadOnlyDictionary<string, object> StatItems { get; }
        IReadOnlyDictionary<string, object> AttributeItems { get; }
    }
}