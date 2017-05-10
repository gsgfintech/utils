using System.Collections.Generic;
using System.Linq;

namespace Capital.GSG.FX.Utils.Core
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

    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
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
        public static bool IsLargerThan<T>(this IEnumerable<T> enumerable, int largerThan)
        {
            return ((enumerable != null) && (enumerable.Count<T>() >= largerThan));
        }

        public static Queue<T> ToSortedQueue<T>(this List<T> list, IComparer<T> sortComparer = null)
        {
            if (list == null)
                return null;

            if (list.Count == 0)
                return new Queue<T>();

            if (sortComparer == null)
                list.Sort();
            else
                list.Sort(sortComparer);

            Queue<T> queue = new Queue<T>();

            foreach (var item in list)
                queue.Enqueue(item);

            return queue;
        }
    }

    public static class DictionaryUtils
    {
        public static TVal GetValueOrDefault<TKey, TVal>(this Dictionary<TKey, TVal> dict, TKey key)
        {
            return dict.GetValueOrDefault(key, default(TVal));
        }

        public static TVal GetValueOrDefault<TKey, TVal>(this Dictionary<TKey, TVal> dict, TKey key, TVal defaultVal)
        {
            if (dict == null || !dict.ContainsKey(key))
                return defaultVal;

            return dict[key];
        }

        public static bool TryGetValue<TKey, TVal>(this Dictionary<TKey, TVal> dict, TKey key, out TVal retVal)
        {
            return dict.TryGetValue(key, default(TVal), out retVal);
        }

        public static bool TryGetValue<TKey, TVal>(this Dictionary<TKey, TVal> dict, TKey key, TVal defaultVal, out TVal retVal)
        {
            if (dict == null || !dict.ContainsKey(key))
            {
                retVal = defaultVal;
                return false;
            }
            else
            {
                retVal = dict[key];
                return true;
            }
        }
    }
}
