using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Teirlinck.Utils
{
    public static class CollectionUtils
    {
        public static bool IsNullOrEmpty<T>(IEnumerable<T> enumerable)
        {
            return ((enumerable == null) || (enumerable.Count<T>() == 0));
        }

        /// <summary>
        /// Returns true if an enumerable is not null and has a size larger than a certain minimum
        /// </summary>
        /// <typeparam name="T">The type of the generic enumerable</typeparam>
        /// <param name="enumerable">The enumerable to test</param>
        /// <param name="largerThan">The minimum sixze (inclusive)</param>
        /// <returns></returns>
        public static bool IsLargerThan<T>(IEnumerable<T> enumerable, int largerThan)
        {
            return ((enumerable != null) && (enumerable.Count<T>() >= largerThan));
        }
    }
}
