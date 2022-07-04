// Author：yaoshuai
// Email：yscode@126.com
// Date：2021-12-29
// ------------------------------

using System.Collections.Generic;

namespace YCSharp
{
    public static class ListExt
    {
        public static T RemoveFirst<T>(this List<T> list, T defaultValue)
        {
            if (list == null || list.Count <= 0) { return defaultValue; }
            T t = list[0];
            list.RemoveAt(0);
            return t;
        }
        public static T RemoveLast<T>(this List<T> list, T defaultValue)
        {
            if (list == null || list.Count <= 0) { return defaultValue; }
            T t = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return t;
        }
        public static T RemoveAtIndex<T>(this List<T> list, int index, T defaultValue)
        {
            if (list == null || list.Count <= 0 || index < 0 || list.Count <= index) { return defaultValue; }
            T t = list[index];
            list.RemoveAt(index);
            return t;
        }

        /// <summary>
        /// 不为空并且有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool HasData<T>(this List<T> list)
        {
            return list != null && list.Count > 0;
        }

        /// <summary>
        /// 为空或者没有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool HasNoData<T>(this List<T> list)
        {
            return list == null || list.Count <= 0;
        }
    }
}