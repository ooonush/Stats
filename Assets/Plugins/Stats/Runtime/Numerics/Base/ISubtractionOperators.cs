#nullable enable

// ReSharper disable TypeParameterCanBeVariant

namespace Stats
{
    public interface ISubtractionOperators<TSelf, TOther, TResult>
        where TSelf : ISubtractionOperators<TSelf, TOther, TResult>?
    {
        TResult Subtract(TOther value);
    }
}