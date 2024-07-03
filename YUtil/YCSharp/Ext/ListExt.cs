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
        public static void For<T>(this List<T> list, Func<bool> breakCondition, Predicate<T> filterCondition, Action<(int index, T element)> doAction)
        {
            if (breakCondition != null && breakCondition.Invoke()) { return; }

            int totalCount = list.Count;
            for (int i = 0; i < totalCount; i++)
            {
                if (breakCondition != null && breakCondition.Invoke()) { break; }

                if (filterCondition == null || filterCondition.Invoke(list[i]))
                {
                    // 没有条件，或有条件并满足
                    doAction?.Invoke((i, list[i]));
                }

                if (breakCondition != null && breakCondition.Invoke()) { break; }
            }
        }
        public static void ForReverse<T>(this List<T> list, Func<bool> breakCondition, Predicate<T> filterCondition, Action<(int index, T element)> doAction)
        {
            if (breakCondition != null && breakCondition.Invoke()) { return; }

            int totalCount = list.Count;
            for (int i = totalCount - 1; i >= 0; i--)
            {
                if (breakCondition != null && breakCondition.Invoke()) { break; }

                if (filterCondition == null || filterCondition.Invoke(list[i]))
                {
                    // 没有条件，或有条件并满足
                    doAction?.Invoke((i, list[i]));
                }

                if (breakCondition != null && breakCondition.Invoke()) { break; }
            }
        }
        public static void RemoveElements<T>(this List<T> list, Predicate<T> filterCondition)
        {
            if (filterCondition != null)
            {
                int totalCount = list.Count;
                for (int i = totalCount - 1; i >= 0; i--)
                {
                    if (filterCondition.Invoke(list[i]))
                    {
                        list.RemoveAt(i);
                    }
                }
            }
        }
        public static void RemoveDuplicate<T>(this List<T> list)
        {
            List<T> newList = new List<T>();
            list.For(null, null, data =>
            {
                if (!newList.Contains(data.element))
                {
                    newList.Add(data.element);
                }
            });
            list.Clear();
            list.AddRange(newList);
        }
        public static void AddIfNotContain<T>(this List<T> list, T element)
        {
            if (!list.Contains(element))
            {
                list.Add(element);
            }
        }
        public static List<T> DeepCopy<T>(this List<T> list, bool allowDuplicate, Predicate<T> filterCondition)
        {
            List<T> newList = new List<T>();
            list.For(null, filterCondition, data =>
            {
                if (allowDuplicate)
                {
                    newList.Add(data.element);
                }
                else if (!allowDuplicate && !newList.Contains(data.element))
                {
                    newList.Add(data.element);
                }
            });
            return newList;
        }
        public static List<T2> Convert<T1, T2>(this List<T1> list, bool allowDuplicate, Predicate<T1> filterCondition, Func<T1, T2> convertCondition)
        {
            List<T2> newList = new List<T2>();
            list.For(null, filterCondition, data =>
            {
                T2 t2 = convertCondition.Invoke(data.element);
                if (allowDuplicate)
                {
                    newList.Add(t2);
                }
                else if (!allowDuplicate && !newList.Contains(t2))
                {
                    newList.Add(t2);
                }
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
        public static void For<T>(this T[] array, Func<bool> breakCondition, Predicate<T> filterCondition, Action<(int index, T element)> doAction)
        {
            if (breakCondition != null && breakCondition.Invoke()) { return; }

            int totalLength = array.Length;
            for (int i = 0; i < totalLength; i++)
            {
                if (breakCondition != null && breakCondition.Invoke()) { break; }

                if (filterCondition == null || filterCondition.Invoke(array[i]))
                {
                    // 没有条件，或有条件并满足
                    doAction?.Invoke((i, array[i]));
                }

                if (breakCondition != null && breakCondition.Invoke()) { break; }
            }
        }
        public static void ForReverse<T>(this T[] array, Func<bool> breakCondition, Predicate<T> filterCondition, Action<(int index, T element)> doAction)
        {
            if (breakCondition != null && breakCondition.Invoke()) { return; }

            int totalLength = array.Length;
            for (int i = totalLength - 1; i >= 0; i--)
            {
                if (breakCondition != null && breakCondition.Invoke()) { break; }

                if (filterCondition == null || filterCondition.Invoke(array[i]))
                {
                    // 没有条件，或有条件并满足
                    doAction?.Invoke((i, array[i]));
                }

                if (breakCondition != null && breakCondition.Invoke()) { break; }
            }
        }
        public static List<T> DeepCopy<T>(this T[] array, bool allowDuplicate, Predicate<T> filterCondition)
        {
            List<T> newList = new List<T>();
            array.For(null, filterCondition, data =>
            {
                if (allowDuplicate)
                {
                    newList.Add(data.element);
                }
                else if (!allowDuplicate && !newList.Contains(data.element))
                {
                    newList.Add(data.element);
                }
            });
            return newList;
        }
        public static List<T2> Convert<T1, T2>(this T1[] array, bool allowDuplicate, Predicate<T1> filterCondition, Func<T1, T2> convertCondition)
        {
            List<T2> newList = new List<T2>();
            array.For(null, filterCondition, data =>
            {
                T2 t2 = convertCondition.Invoke(data.element);
                if (allowDuplicate)
                {
                    newList.Add(t2);
                }
                else if (!allowDuplicate && !newList.Contains(t2))
                {
                    newList.Add(t2);
                }
            });
            return newList;
        }
    }
}