// Author：yaoshuai
// Email：yscode@126.com
// Date：2024-6-28
// ------------------------------

using System;
using System.Collections.Generic;

namespace YCSharp
{
    public static class ListExt
    {
        public static bool HasElements<T>(this List<T> list)
        {
            return list != null && list.Count > 0;
        }
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }
        public static void For<T>(this List<T> list, Func<T, bool> condition, Action<(int index, T element)> doAction)
        {
            int totalCount = list.Count;
            for (int i = 0; i < totalCount; i++)
            {
                if (condition == null || condition.Invoke(list[i]))
                {
                    // 没有条件，或有条件并满足
                    doAction?.Invoke((i, list[i]));
                }
            }
        }
        public static void ForReverse<T>(this List<T> list, Func<T, bool> condition, Action<(int index, T element)> doAction)
        {
            int totalCount = list.Count;
            for (int i = totalCount - 1; i >= 0; i--)
            {
                if (condition == null || condition.Invoke(list[i]))
                {
                    // 没有条件，或有条件并满足
                    doAction?.Invoke((i, list[i]));
                }
            }
        }
        public static void RemoveElements<T>(this List<T> list, Func<T, bool> condition)
        {
            if (condition != null)
            {
                int totalCount = list.Count;
                for (int i = totalCount - 1; i >= 0; i--)
                {
                    if (condition.Invoke(list[i]))
                    {
                        list.RemoveAt(i);
                    }
                }
            }
        }
        public static List<T> DeepCopy<T>(this List<T> list, Func<T, bool> condition)
        {
            List<T> newList = new List<T>();
            list.For(condition, data =>
            {
                newList.Add(data.element);
            });
            return newList;
        }
    }
    public static class ArrayExt
    {
        public static bool HasElements<T>(this T[] array)
        {
            return array != null && array.Length > 0;
        }
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }
        public static void For<T>(this T[] array, Func<T, bool> condition, Action<(int index, T element)> doAction)
        {
            int totalLength = array.Length;
            for (int i = 0; i < totalLength; i++)
            {
                if (condition == null || condition.Invoke(array[i]))
                {
                    // 没有条件，或有条件并满足
                    doAction?.Invoke((i, array[i]));
                }
            }
        }
        public static void ForReverse<T>(this T[] array, Func<T, bool> condition, Action<(int index, T element)> doAction)
        {
            int totalLength = array.Length;
            for (int i = totalLength - 1; i >= 0; i--)
            {
                if (condition == null || condition.Invoke(array[i]))
                {
                    // 没有条件，或有条件并满足
                    doAction?.Invoke((i, array[i]));
                }
            }
        }
        public static List<T> DeepCopy<T>(this T[] array, Func<T, bool> condition)
        {
            List<T> list = new List<T>();
            array.For(condition, data =>
            {
                list.Add(data.element);
            });
            return list;
        }
    }
}