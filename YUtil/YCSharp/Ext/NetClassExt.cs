// Author：yaoshuai
// Email：yscode@126.com
// Date：2022-2-10
// ------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace YCSharp
{
    public static class NetClassExt
    {
        public static bool IsNull<T>(this T obj) where T : class
        {
            return obj == null;
        }

        public static bool IsNotNull<T>(this T obj) where T : class
        {
            return obj != null;
        }

        /// 是否含有空元素
        public static bool HasNullItem<T>(this T[] objList) where T : class
        {
            if (objList == null) { return false; }
            foreach (T obj in objList)
            {
                if (obj == null) { return true; }
            }
            return false;
        }

        /// 是否含有空元素
        public static bool HasNullItem<T>(this List<T> objList) where T : class
        {
            if (objList == null) { return false; }
            foreach (T obj in objList)
            {
                if (obj == null) { return true; }
            }
            return false;
        }

        /// 是否含有空元素
        public static bool HasNullItem(this ArrayList objList)
        {
            if (objList == null) { return false; }
            foreach (object obj in objList)
            {
                if (obj == null) { return true; }
            }
            return false;
        }

        /// 是否含有空元素
        public static bool HasNullItem(this Array objList)
        {
            if (objList == null) { return false; }
            foreach (object obj in objList)
            {
                if (obj == null) { return true; }
            }
            return false;
        }
    }
}