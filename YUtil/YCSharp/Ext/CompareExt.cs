using System;
using System.Collections.Generic;

namespace YCSharp
{
    public static class CompareExt
    {
        public static T MaxValue<T>(this List<T> list) where T : IComparable
        {
            T max = list[0];
            foreach (var item in list)
            {
                if (max.CompareTo(item) < 0)
                {
                    max = item;
                }
            }
            return max;
        }
        public static T MaxValue<T>(this T[] array) where T : IComparable
        {
            T max = array[0];
            foreach (var item in array)
            {
                if (max.CompareTo(item) < 0)
                {
                    max = item;
                }
            }
            return max;
        }
        public static T MinValue<T>(this List<T> list) where T : IComparable
        {
            T max = list[0];
            foreach (var item in list)
            {
                if (max.CompareTo(item) > 0)
                {
                    max = item;
                }
            }
            return max;
        }
        public static T MinValue<T>(this T[] array) where T : IComparable
        {
            T max = array[0];
            foreach (var item in array)
            {
                if (max.CompareTo(item) > 0)
                {
                    max = item;
                }
            }
            return max;
        }
    }
}