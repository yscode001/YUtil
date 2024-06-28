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
        /// <summary>
        /// 有元素(不为空且数量大于0)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool HasElements<T>(this List<T> list)
        {
            return list != null && list.Count > 0;
        }

        /// <summary>
        /// 没有元素(为空或数量等于0)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool HasNoElement<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }

        /// <summary>
        /// 移除指定元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="condition"></param>
        public static void RemoveElement<T>(this List<T> list, Func<T, bool> condition)
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

        /// <summary>
        /// 正向遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="condition"></param>
        /// <param name="doAction"></param>
        public static void For<T>(this List<T> list, Func<T, bool> condition, Action<(int index, T element)> doAction)
        {
            int totalCount = list.Count;
            for (int i = 0; i < totalCount; i++)
            {
                if (condition == null)
                {
                    doAction?.Invoke((i, list[i]));
                }
                else if (condition.Invoke(list[i]))
                {
                    doAction?.Invoke((i, list[i]));
                }
            }
        }

        /// <summary>
        /// 反向遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="condition"></param>
        /// <param name="doAction"></param>
        public static void ForReverse<T>(this List<T> list, Func<T, bool> condition, Action<(int index, T element)> doAction)
        {
            int totalCount = list.Count;
            for (int i = totalCount - 1; i >= 0; i--)
            {
                if (condition == null)
                {
                    doAction?.Invoke((i, list[i]));
                }
                else if (condition.Invoke(list[i]))
                {
                    doAction?.Invoke((i, list[i]));
                }
            }
        }
    }
    public static class ArrayExt
    {
        /// <summary>
        /// 有元素(不为空且数量大于0)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool HasElements<T>(this T[] array)
        {
            return array != null && array.Length > 0;
        }

        /// <summary>
        /// 没有元素(为空或数量等于0)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool HasNoElement<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }

        /// <summary>
        /// 正向遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <param name="doAction"></param>
        public static void For<T>(this T[] array, Func<T, bool> condition, Action<(int index, T element)> doAction)
        {
            int totalLength = array.Length;
            for (int i = 0; i < totalLength; i++)
            {
                if (condition == null)
                {
                    doAction?.Invoke((i, array[i]));
                }
                else if (condition.Invoke(array[i]))
                {
                    doAction?.Invoke((i, array[i]));
                }
            }
        }

        /// <summary>
        /// 反向遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <param name="doAction"></param>
        public static void ForReverse<T>(this T[] array, Func<T, bool> condition, Action<(int index, T element)> doAction)
        {
            int totalLength = array.Length;
            for (int i = totalLength - 1; i >= 0; i--)
            {
                if (condition == null)
                {
                    doAction?.Invoke((i, array[i]));
                }
                else if (condition.Invoke(array[i]))
                {
                    doAction?.Invoke((i, array[i]));
                }
            }
        }
    }
}