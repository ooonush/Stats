using System;

namespace Stats
{
    public interface IStatNumberBase<TSelf>
        : IAdditionOperators<TSelf, TSelf, TSelf>,
            IDecrementOperators<TSelf>,
            IDivisionOperators<TSelf, TSelf, TSelf>,
            IEquatable<TSelf>,
            IIncrementOperators<TSelf>,
            IMultiplyOperators<TSelf, TSelf, TSelf>,
            ISubtractionOperators<TSelf, TSelf, TSelf>
        where TSelf : IStatNumberBase<TSelf>
    {

    }
}