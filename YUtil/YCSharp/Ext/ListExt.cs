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
        /// 正向遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="doAction"></param>
        public static void Foreach<T>(this List<T> list, Action<(int index, T element)> doAction)
        {
            int totalCount = list.Count;
            for (int i = 0; i < totalCount; i++)
            {
                doAction?.Invoke((i, list[i]));
            }
        }

        /// <summary>
        /// 反向遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="doAction"></param>
        public static void ForeachReverse<T>(this List<T> list, Action<(int index, T element)> doAction)
        {
            int totalCount = list.Count;
            for (int i = totalCount - 1; i >= 0; i--)
            {
                doAction?.Invoke((i, list[i]));
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
        /// <param name="doAction"></param>
        public static void Foreach<T>(this T[] array, Action<(int index, T element)> doAction)
        {
            int totalLength = array.Length;
            for (int i = 0; i < totalLength; i++)
            {
                doAction?.Invoke((i, array[i]));
            }
        }

        /// <summary>
        /// 反向遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="doAction"></param>
        public static void ForeachReverse<T>(this T[] array, Action<(int index, T element)> doAction)
        {
            int totalLength = array.Length;
            for (int i = totalLength - 1; i >= 0; i--)
            {
                doAction?.Invoke((i, array[i]));
            }
        }
    }
}