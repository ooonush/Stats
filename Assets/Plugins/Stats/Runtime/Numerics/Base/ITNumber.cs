using System;

namespace Stats
{
    public interface ITNumber<TSelf>
        : IComparable,
            IComparable<TSelf>,
            IComparisonOperators<TSelf, TSelf, bool>,
            IStatNumberBase<TSelf>
        where TSelf : ITNumber<TSelf>
    {
    }
}