﻿using System.Collections.Generic;
using System.Linq;

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
        public static T TryGetValue<T>(this Dictionary<string, T> dict, string key)
        {
            return dict.TryGetValue<T>(key, default(T));
        }

        public static T TryGetValue<T>(this Dictionary<string, T> dict, string key, T defaultVal)
        {
            if (string.IsNullOrEmpty(key))
                return defaultVal;

            if (dict == null || !dict.ContainsKey(key))
                return defaultVal;

            return dict[key];
        }
    }
}
