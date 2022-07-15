// Author：yaoshuai
// Email：yscode@126.com
// Date：2021-12-29
// ------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace YCSharp
{
    public static class ListExt
    {
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
        /// 为空或没有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool HasNoData<T>(this List<T> list)
        {
            return list == null || list.Count <= 0;
        }

        /// <summary>
        /// 不为空并且有数据
        /// </summary>
        /// <param name="arrayList"></param>
        /// <returns></returns>
        public static bool HasData(this ArrayList arrayList)
        {
            return arrayList != null && arrayList.Count > 0;
        }

        /// <summary>
        /// 为空或没有数据
        /// </summary>
        /// <param name="arrayList"></param>
        /// <returns></returns>
        public static bool HasNoData(this ArrayList arrayList)
        {
            return arrayList == null || arrayList.Count <= 0;
        }

        /// <summary>
        /// 不为空并且有数据
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool HasData(this Array array)
        {
            return array != null && array.Length > 0;
        }

        /// <summary>
        /// 为空或没有数据
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool HasNoData(this Array array)
        {
            return array == null || array.Length <= 0;
        }
    }
}