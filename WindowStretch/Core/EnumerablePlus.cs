using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowStretch.Core
{
    public static class EnumerablePlus
    {
        ///<summary>条件付きMin。</summary>
        public static T MinBy<T, U>(this IEnumerable<T> xs, Func<T, U> key) where U : IComparable<U>
        {
            return xs.Aggregate((a, b) => key(a).CompareTo(key(b)) < 0 ? a : b);
        }
    }
}
