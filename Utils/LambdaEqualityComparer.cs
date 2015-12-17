using System;
using System.Collections.Generic;

namespace Net.Teirlinck.Utils
{
    public class LambdaEqualityComparer<T> : IEqualityComparer<T>
    {
        private Func<T, T, bool> _equalsFunction;
        private Func<T, int> _hashCodeFunction;

        public LambdaEqualityComparer(Func<T, T, bool> equalsFunction, Func<T, int> hashCodeFunction)
        {
            if (equalsFunction == null)
                throw new ArgumentNullException(nameof(equalsFunction));

            if (hashCodeFunction == null)
                throw new ArgumentNullException(nameof(hashCodeFunction));

            _equalsFunction = equalsFunction;
            _hashCodeFunction = hashCodeFunction;
        }

        public bool Equals(T x, T y)
        {
            return _equalsFunction(x, y);
        }

        public int GetHashCode(T obj)
        {
            return _hashCodeFunction(obj);
        }
    }
}
