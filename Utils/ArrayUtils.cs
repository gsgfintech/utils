using System;

namespace Net.Teirlinck.Utils
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

            if (length < 1 || index + length > sourceArray.Length - 1)
                throw new ArgumentException(nameof(length));

            T[] result = new T[length];
            Array.Copy(sourceArray, index, result, 0, length);
            return result;
        }
    }
}
