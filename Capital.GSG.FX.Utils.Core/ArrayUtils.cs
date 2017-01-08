using System;
using System.Collections.Generic;

namespace Capital.GSG.FX.Utils.Core
{
    public static class ArrayUtils
    {
        /// <summary>
        /// Returns a sub-array of an array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceArray">The source array</param>
        /// <param name="index">Start index (inclusive)</param>
        /// <param name="length">Length of the sub-array</param>
        /// <returns></returns>
        public static T[] SubArray<T>(this T[] sourceArray, int index, int length)
        {
            if (sourceArray == null || sourceArray.Length == 0)
                throw new ArgumentException(nameof(sourceArray));

            if (index < 0 || index > sourceArray.Length - 1)
                throw new ArgumentException(nameof(index));

            if (length < 1 || index + length > sourceArray.Length)
                throw new ArgumentException(nameof(length));

            T[] result = new T[length];
            Array.Copy(sourceArray, index, result, 0, length);
            return result;
        }

        /// <summary>
        /// Returns a sub-array of an array (starting from the start index to the end of the array)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceArray">The source array</param>
        /// <param name="index">Start index (inclusive)</param>
        /// <returns></returns>
        public static T[] SubArray<T>(this T[] sourceArray, int index)
        {
            int length = sourceArray.Length - index;

            return sourceArray.SubArray(index, length);
        }

        public static List<T[]> SplitArray<T>(this T[] sourceArray, int subArraySize)
        {
            if (sourceArray.IsNullOrEmpty())
                return null;

            List<T[]> result = new List<T[]>();

            for (int i = 0; i < sourceArray.Length; i = i + subArraySize)
            {
                if (sourceArray.Length < i + subArraySize)
                    subArraySize = sourceArray.Length - i;

                T[] val = new T[subArraySize];

                Array.Copy(sourceArray, i, val, 0, subArraySize);

                result.Add(val);
            }

            return result;
        }
    }
}
